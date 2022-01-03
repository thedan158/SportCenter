using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Microsoft.Win32;
using SportCenter.Model;

namespace SportCenter.ViewModel
{
    public class EmployeeViewModel : BaseViewModel
    {
        private ObservableCollection<employee> _Listemployee;
        public ObservableCollection<employee> Listemployee { get => _Listemployee; set { _Listemployee = value; OnPropertyChanged(); } }

        //Storage VM

        private string imageFileName;
        private byte[] _imageemployee;
        public byte[] imageemployee { get => _imageemployee; set { _imageemployee = value; OnPropertyChanged(); } }
        public Func<double, string> YFormatter { get; set; }
        private int _idemployee;
        public int idemployee { get => _idemployee; set { _idemployee = value; OnPropertyChanged(); } }

        private string _nameemployee;
        public string nameemployee { get => _nameemployee; set { _nameemployee = value; OnPropertyChanged(); } }

        private decimal? _salaryemployee;
        public decimal? salaryemployee { get => _salaryemployee; set { _salaryemployee = value; OnPropertyChanged(); } }

        private string _roleemployee;
        public string roleemployee { get => _roleemployee; set { _roleemployee = value; OnPropertyChanged(); } }

        private string _phoneemployee;
        public string phoneemployee { get => _phoneemployee; set { _phoneemployee = value; OnPropertyChanged(); } }

        private DateTime _dateOfBirthemployee;
        public DateTime dateOfBirthemployee { get => _dateOfBirthemployee; set { _dateOfBirthemployee = value; OnPropertyChanged(); } }

        public ICommand addCommand { get; set; }
        public ICommand editCommand { get; set; }
        public ICommand deleteCommand { get; set; }
        public ICommand ReloadCommand { get; set; }

        public ICommand SelectImageCommand { get; set; }

        private employee _SelectedItem;
        public employee SelectedItem
        {
            get => _SelectedItem;
            set
            {
                _SelectedItem = value;
                OnPropertyChanged();
                if (SelectedItem != null)
                {
                    idemployee = SelectedItem.id;
                    nameemployee = SelectedItem.name;
                    salaryemployee = SelectedItem.salary;
                    roleemployee = SelectedItem.role;
                    phoneemployee = SelectedItem.phoneNumber;
                    dateOfBirthemployee = (DateTime)SelectedItem.dateOfBirth;
                    imageemployee = SelectedItem.imageFile;                   
                }    
            }
        }
        public EmployeeViewModel()
        {
            Listemployee = new ObservableCollection<employee>(DataProvider.Ins.DB.employees);
            LoadEmployeeData();
            SelectImageCommand = new RelayCommand<Grid>((parameter) => true, (parameter) => ChooseImage(parameter));
            ReloadCommand = new RelayCommand<Grid>((parameter) => true, (parameter) => ReloadEmployee(parameter));
            // Add goods
            addCommand = new RelayCommand<Grid>((parameter) =>
            {
                if (string.IsNullOrEmpty(nameemployee))
                {
                    return false;
                }

                var nameList = DataProvider.Ins.DB.employees.Where(p => p.name == nameemployee);
                if (nameList == null || nameList.Count() != 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            },
            (parameter) => AddEmployee(parameter));


            //Edit goods
            editCommand = new RelayCommand<Grid>((parameter) =>
            {

                if (string.IsNullOrEmpty(nameemployee) || SelectedItem == null)
                    return false;
                var nameList = DataProvider.Ins.DB.employees.Where(p => p.id == idemployee);
                if (nameList != null && nameList.Count() != 0)
                    return true;
                return false;
            }, (parameter) =>
            {
                MessageBoxResult result = MessageBox.Show("Confirm edit?", "Note", MessageBoxButton.YesNo);
                Listemployee = new ObservableCollection<employee>(DataProvider.Ins.DB.employees);

                if (result == MessageBoxResult.Yes)
                {
                    if (imageFileName != null)
                    {
                        byte[] imgByteArr;
                        imgByteArr = Converter.Instance.ConvertImageToBytes(imageFileName);
                        var employee = DataProvider.Ins.DB.employees.Where(x => x.id == SelectedItem.id).SingleOrDefault();
                        employee.name = nameemployee;
                        employee.role = roleemployee;
                        employee.phoneNumber = phoneemployee;
                        employee.imageFile = imgByteArr;
                        employee.dateOfBirth = dateOfBirthemployee;
                        employee.salary = salaryemployee;

                        DataProvider.Ins.DB.SaveChanges();
                        parameter.Background = null;

                        ImageBrush image = new ImageBrush();
                        image.ImageSource = ByteToImage(employee.imageFile);
                        parameter.Background = image;
                        parameter.Children[0].Visibility = Visibility.Visible;
                        //parameter.Children[2].Visibility = Visibility.Visible;
                        MessageBox.Show("Edit employee successfully!!");
                    }
                    else
                    {
                        var employee = DataProvider.Ins.DB.employees.Where(x => x.id == SelectedItem.id).SingleOrDefault();
                        employee.name = nameemployee;
                        employee.role = roleemployee;
                        employee.phoneNumber = phoneemployee;
                        employee.dateOfBirth = dateOfBirthemployee;
                        employee.salary = salaryemployee;


                        DataProvider.Ins.DB.SaveChanges();

                        LoadEmployeeData();
                        MessageBox.Show("Edit employee successfully!!");
                    }
                }

            });

            //Delete goods
            deleteCommand = new RelayCommand<Grid>((parameter) => true,
            (parameter) =>
            {
                MessageBoxResult result = MessageBox.Show("Confirm delete?", "Note", MessageBoxButton.YesNo);

                Listemployee = new ObservableCollection<employee>(DataProvider.Ins.DB.employees);
                if (result == MessageBoxResult.Yes)
                {
                    var employee = DataProvider.Ins.DB.employees.Where(x => x.id == SelectedItem.id).SingleOrDefault();
                    DataProvider.Ins.DB.employees.Remove(employee);
                    DataProvider.Ins.DB.SaveChanges();
                    Listemployee.Remove(employee);
                    idemployee = 0;
                    nameemployee = null;
                    salaryemployee = null;
                    phoneemployee = null;
                    roleemployee = null;
                    dateOfBirthemployee = DateTime.Now;
                    MessageBox.Show("Delete employee successfully!!");
                }
            });
        }

        public void ReloadEmployee(Grid parameter)
        {
                     
            foreach (var item in Listemployee)
            {
                if(item == SelectedItem)
                {
                    ImageBrush image = new ImageBrush
                    {
                        ImageSource = ByteToImage(item.imageFile)
                    };
                    parameter.Background = image;
                }
            }
        }
        
        public static ImageSource ByteToImage(byte[] imageData)
            {
                BitmapImage biImg = new BitmapImage();
                MemoryStream ms = new MemoryStream(imageData);
                biImg.BeginInit();
                biImg.StreamSource = ms;
                biImg.EndInit();

                ImageSource imgSrc = biImg as ImageSource;

                return imgSrc;
            }
        

        private void ChooseImage(Grid parameter)
        {

            OpenFileDialog op = new OpenFileDialog();
            op.Title = "Chọn ảnh";
            op.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" + "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" + "Portable Network Graphic (*.png)|*.png";
            if (op.ShowDialog() == true)
            {
                imageFileName = op.FileName;
                ImageBrush imageBrush = new ImageBrush();
                BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.CacheOption = BitmapCacheOption.OnLoad;
                bitmap.UriSource = new Uri(imageFileName);
                bitmap.EndInit();
                imageBrush.ImageSource = bitmap;
                parameter.Background = imageBrush;
                //if (parameter.Children.Count > 1)
                //{
                //    parameter.Children[0].Visibility = Visibility.Hidden;
                //    parameter.Children[2].Visibility = Visibility.Hidden;

                //}
            }
        }

        internal void LoadEmployeeData()
        {

            Listemployee = new ObservableCollection<employee>();
            var listemployee = DataProvider.Ins.DB.employees;
            foreach (var item in listemployee)
            {
                _ = new employee();

                employee Storage = item;

                Listemployee.Add(Storage);
            }
            
        }
        public void AddEmployee(Grid parameter)
        {
            {



                Listemployee = new ObservableCollection<employee>(DataProvider.Ins.DB.employees);
                if (imageFileName == null)
                {
                    MessageBox.Show("Please choose image!!");
                }
                else
                {

                    byte[] imgByteArr;
                    imgByteArr = Converter.Instance.ConvertImageToBytes(imageFileName);
                    var employee = new employee()
                    {
                        name = nameemployee,
                        id = idemployee,
                        salary = salaryemployee,
                        phoneNumber = phoneemployee,
                        dateOfBirth = dateOfBirthemployee
                        ,
                        role = roleemployee,
                        imageFile = imgByteArr
                    };
                    DataProvider.Ins.DB.employees.Add(employee);
                    DataProvider.Ins.DB.SaveChanges();
                    Listemployee.Add(employee);
                    idemployee = 0;
                    nameemployee = null;
                    phoneemployee = null;
                    salaryemployee = null;
                    roleemployee = null;
                    dateOfBirthemployee = DateTime.Now;
                    parameter.Background = null;
                    parameter.Children[0].Visibility = Visibility.Visible;
                    //parameter.Children[2].Visibility = Visibility.Visible;
                    MessageBox.Show("Add employee successfully!!");
                }

            }
        }

    }
}
