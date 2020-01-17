using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DataBinding_6
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DAL DAL_Object = new DAL();
        //List<Person> Personer = new List<Person>();
        //Person person = new Person(0, "Svend", "Bendt", 100);
        ObservableCollection<Person> Personer = new ObservableCollection<Person>();
        Person person = new Person();

        public MainWindow()
        {
            InitializeComponent();

            //Personer = DAL_Object.Get().ToList();
            Personer = DAL_Object.Get();

            person = Personer.FirstOrDefault();
            contentControl1.DataContext = Personer.FirstOrDefault();
            contentControl2.DataContext = Personer.FirstOrDefault();
            this.DataContext = Personer;

            cmbPersons.SelectedValue = Personer.FirstOrDefault();
            //cmbPersons.Items.Add("1");
        }

        private void btnVisData_Click(object sender, RoutedEventArgs e)
        {
            string PersonData = person.ID + 
                " : " +
                person.Fornavn +
                " " +
                person.Efternavn +
                " har en formue på " +
                person.Formue +
                " Kr.";

            MessageBox.Show(PersonData);
        }

        private void btnOpdaterData_Click(object sender, RoutedEventArgs e)
        {
            person.Formue++;
            person.Fornavn += "1";
            person.Efternavn += "2";
        }

        private void cmbPersons_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (null != cmbPersons.SelectedValue)
            {
                contentControl1.DataContext = Personer.FirstOrDefault(p => p.ID == Convert.ToInt32(cmbPersons.SelectedValue.ToString()));
            }
        }

        private void btnSletData_Click(object sender, RoutedEventArgs e)
        {
            Person Person_Object = (Person)contentControl1.DataContext;

            DAL_Object.Delete(Person_Object);
            cmbPersons.SelectedValue = Personer.FirstOrDefault();
        }

        private void btnInsertData_Click(object sender, RoutedEventArgs e)
        {
            var Test = (Person)contentControl2.DataContext;
            Person Person_Object = (Person)contentControl2.DataContext;
            DAL_Object.Insert(Fornavn : Person_Object.Fornavn,
                              Efternavn: Person_Object.Efternavn,
                              Formue: Person_Object.Formue);
        }
    }
}
