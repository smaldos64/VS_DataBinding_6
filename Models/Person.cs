using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBinding_6.Models
{
    public class Person : INotifyPropertyChanged
    {
        private int _iD;
        public int ID
        {
            get
            {
                return this._iD;
            }
            set
            {
                this._iD = value;
                OnPropertyChanged("ID");
            }
        }

        private string _fornavn;
        public string Fornavn
        {
            get
            {
                return this._fornavn;
            }

            set
            {
                this._fornavn = value;
                OnPropertyChanged("Fornavn");
            }
        }

        private string _efternavn;
        public string Efternavn 
        { 
            get
            {
                return this._efternavn;
            }
            set
            {
                this._efternavn = value;
                OnPropertyChanged("Efternavn");
            }
        }
        
        private int _formue;
        public int Formue 
        { 
            get
            {
                return this._formue;
            }
            
            set
            {
                this._formue = value;
                OnPropertyChanged("Formue");
            } 
        }

        public Person(int ID, string Fornavn, string Efternavn, int Formue)
        {
            this.ID = ID;
            this.Fornavn = Fornavn;
            this.Efternavn = Efternavn;
            this.Formue = Formue;
        }

        public Person()
        {
            this.ID = -1;
            this.Fornavn = "";
            this.Efternavn = "";
            this.Formue = 0;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string PropertyNavn)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(PropertyNavn));
            }
        }

        public override string ToString()
        {
            //return this.ID + " : " + this.Fornavn + " " + this.Efternavn + " har " + this.Formue.ToString() + "Kr.";
            return this.ID.ToString();
        }

    }
}

