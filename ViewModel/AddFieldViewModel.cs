using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using SportCenter.Model;

namespace SportCenter.ViewModel
{
    public class AddFieldViewModel : BaseViewModel
    {
        public ICommand AddFieldCommand { get; set; }
        private string _addfieldname;
        public string addfieldname { get => _addfieldname; set { _addfieldname = value; OnPropertyChanged(); } }
        private int? _addfieldtype;
        public int? addfieldtype { get => _addfieldtype; set { _addfieldtype = value; OnPropertyChanged(); } }
        private string _addfieldcondition;
        public string addfieldcondition { get => _addfieldcondition; set { _addfieldcondition = value; OnPropertyChanged(); } }

        private ObservableCollection<fieldtype> _fieldtype;
        public ObservableCollection<fieldtype> Listfieldtype { get => _fieldtype; set { _fieldtype = value; OnPropertyChanged(); } }
        public List<string> displayComboxFieldType;
        public ICommand CancleCommand { get; set; }


        public AddFieldViewModel()
        {
            _fieldtype = new ObservableCollection<fieldtype>(DataProvider.Ins.DB.fieldtypes);
            displayComboxFieldType = new List<string>();
            AddFieldCommand = new RelayCommand<object>((parameter) => true, (parameter) =>
            {
                AddFieldFunction();
            }
            );


            Update_filedType();

        }



        private void Update_filedType()
        {
            if (_fieldtype == null)
            {
                return;
            }
            else
            {
                foreach (var item in _fieldtype)
                {
                    string temp = "";
                    temp = item.name;
                    displayComboxFieldType.Add(temp);
                }
            }
        }
        // Selected field type OPGG
        private int _Selectedidfieldtype;
        public int Selectedidfieldtype { get => _Selectedidfieldtype; set { _Selectedidfieldtype = value; OnPropertyChanged(); } }

        private fieldtype _selecttype = null;
        public fieldtype selecttedype
        {
            get => _selecttype;
            set
            {
                _selecttype = value;
                OnPropertyChanged();
                if (selecttedype != null)
                {
                    Selectedidfieldtype = selecttedype.id;
                }
            }
        }

        // Selected field condition OP but not GG
        private string _SelectConditionField;
        public string SelectConditionField { get => _SelectConditionField; set { _SelectConditionField = value; OnPropertyChanged(); } }
        private string _selectcondition = null;
        public string selectcondition
        {
            get => _selectcondition;
            set
            {
                _selectcondition = value;
                OnPropertyChanged();
                if (selectcondition != null)
                {
                    SelectConditionField = selectcondition;
                }
            }
        }

        private void AddFieldFunction()
        {
            field temp = new field();
            temp.name = addfieldname;
            temp.idType = Selectedidfieldtype;
            temp.condition = SelectConditionField;
            if (temp.name == null || temp.idType.ToString() == null || temp.condition == null)
            {
                MessageBox.Show("Please filll all information!");
                return;
            }
            ObservableCollection<field> List = new ObservableCollection<field>(DataProvider.Ins.DB.fields.Where(x => x.idType == temp.idType));
            foreach(var item in List)
            {
                if (item.name == temp.name)
                {
                    MessageBox.Show("This name is already exists");
                    return;
                }
            }
            DataProvider.Ins.DB.fields.Add(temp);
            DataProvider.Ins.DB.SaveChanges();
            MessageBox.Show("Added Done.");
        }
    }

}
