using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Diagnostics;
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

using DataBinding_6.Models;
using DataBinding_6.Tools;
using DataBinding_6.Converters;

namespace DataBinding_6
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DAL DAL_Object = new DAL();
        //List<Person> Personer = new List<Person>();
        ObservableCollection<Person> Personer = new ObservableCollection<Person>();
        //Person person = new Person();

        public MainWindow()
        {
            InitializeComponent();

            //Personer = DAL_Object.Get().ToList();
            Personer = DAL_Object.Get();

            contentControl1.DataContext = Personer.FirstOrDefault();
            contentControl2.DataContext = Personer.FirstOrDefault();
            this.DataContext = Personer;

            SetupComboBoxBinding();
        }

        //private void SetComboBoxConverter()
        //{
        //    Binding Bind_Object = new Binding();

        //    Bind_Object.Converter = new PersonConverter();
        //    Bind_Object.Source = Personer.ToList();
        //    Bind_Object.Path = new PropertyPath("Value");
        //}

        private void SetupComboBoxBinding(bool SetFocusToFirstItem = true)
        {
            cmbPersons.ItemsSource = Personer.ToList();
            if (true == SetFocusToFirstItem)
            {
                //cmbPersons.SelectedValue = Personer.FirstOrDefault();
                cmbPersons.SelectedItem = Personer.FirstOrDefault();
                //cmbPersons.Text = Personer[0].ID.ToString() + " : " + Personer[0].Fornavn;
            }
            
            contentControl1.DataContext = Personer.FirstOrDefault(p => p.ID == Convert.ToInt32(cmbPersons.SelectedValue.ToString()));
        }

        private void RemoveSelectionChangedBindings()
        {
            cmbPersons.SelectionChanged -= cmbPersons_SelectionChanged;
            dataGrid1.SelectionChanged -= dataGrid1_SelectionChanged;
            listView1.SelectionChanged -= listView1_SelectionChanged;
        }

        private void InsertSelectionChangedBindings()
        {
            cmbPersons.SelectionChanged += cmbPersons_SelectionChanged;
            dataGrid1.SelectionChanged += dataGrid1_SelectionChanged;
            listView1.SelectionChanged += listView1_SelectionChanged;
        }

        private void btnOpdaterData_Click(object sender, RoutedEventArgs e)
        {
            Person Person_Object = (Person)contentControl1.DataContext;

            DAL_Object.Update(ID: Person_Object.ID, 
                              Fornavn: Person_Object.Fornavn,
                              Efternavn: Person_Object.Efternavn,
                              Formue: Person_Object.Formue);
            SetupComboBoxBinding();
        }

        private void cmbPersons_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (null != cmbPersons.SelectedValue)
            {
                contentControl1.DataContext = Personer.FirstOrDefault(p => p.ID == Convert.ToInt32(cmbPersons.SelectedValue.ToString()));
                //contentControl1.DataContext = Personer.FirstOrDefault(p => p.ID == cmbPersons.SelectedValue.ToString().FindIndexFromString());
            }
        }

        private void btnSletData_Click(object sender, RoutedEventArgs e)
        {
            Person Person_Object = (Person)contentControl1.DataContext;

            RemoveSelectionChangedBindings();
            DAL_Object.Delete(Person_Object);
            //InsertSelectionChangedBindings();
            cmbPersons.ItemsSource = Personer.ToList();
            SetupComboBoxBinding();
            InsertSelectionChangedBindings();
        }

        private void btnInsertData_Click(object sender, RoutedEventArgs e)
        {
            TextBox txtFornavn = FindElementByName<TextBox>(contentControl2, "txtFornavn");
            TextBox txtEfternavn = FindElementByName<TextBox>(contentControl2, "txtEfternavn");
            TextBox txtFormue = FindElementByName<TextBox>(contentControl2, "txtFormue");

            if (DAL_Object.Insert(Fornavn: txtFornavn.Text,
                                  Efternavn: txtEfternavn.Text,
                                  Formue: Convert.ToInt32(txtFormue.Text)) >= 0)
            {
                SetupComboBoxBinding(false);
                txtFornavn.Text = "";
                txtEfternavn.Text = "";
                txtFormue.Text = "";
            }
        }

        private void dataGrid1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int Index = FindPersonIDInList(((Person)dataGrid1.SelectedItem).ID);

            if (Index >= 0)
            {
                cmbPersons.SelectedValue = Personer[Index];
            }
            //cmbPersons.SelectedValue = Personer[(((Person)dataGrid1.SelectedItem).ID)];
        }

        private void listView1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int Index = FindPersonIDInList(((Person)listView1.SelectedItem).ID);

            if (Index >= 0)
            {
                cmbPersons.SelectedValue = Personer[Index];
            }
            //cmbPersons.SelectedValue = Personer[(((Person)listView1.SelectedItem).ID)];
        }

        private int FindPersonIDInList(int PersonID)
        {
            int Index = -1;

            Index = Personer.FindIndex(PersonID);

            return (Index);
        }

        public T FindElementByName<T>(FrameworkElement element, string sChildName) where T : FrameworkElement
        {
            T childElement = null;
            var nChildCount = VisualTreeHelper.GetChildrenCount(element);
            for (int i = 0; i < nChildCount; i++)
            {
                FrameworkElement child = VisualTreeHelper.GetChild(element, i) as FrameworkElement;

                if (child == null)
                    continue;

                if (child is T && child.Name.Equals(sChildName))
                {
                    childElement = (T)child;
                    break;
                }

                childElement = FindElementByName<T>(child, sChildName);

                if (childElement != null)
                    break;
            }
            return childElement;
        }
    }

    //public class DebugDummyConverter : IValueConverter
    //{
    //    public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
    //    {
    //        Debugger.Break();
    //        return value;
    //    }

    //    public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
    //    {
    //        Debugger.Break();
    //        return value;
    //    }
    //}
}
