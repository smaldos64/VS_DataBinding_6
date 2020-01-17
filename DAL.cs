using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace DataBinding_6
{
    public class DAL
    {
        private ObservableCollection<Person> DataBase; // Da vi ikke har adgang til en database, 
                                                       // simulerer vi med denne private liste....

        private ObservableCollection<Person> _publicListe; // Dette er objektet med elementer vi 
                                                           // "deler ud" til brugeren af vores class.

        //Constructoren genererer data til vores falske database
        public DAL()
        {
            DataBase = new ObservableCollection<Person>();
            DataBase.Add(new Person(0, "Svend", "Bendt", 1234));
            DataBase.Add(new Person(1, "Bein", "Stagge", -987654321));
            DataBase.Add(new Person(2, "Turt", "Khorsen", 0));
            DataBase.Add(new Person(3, "Gill", "Bates", int.MaxValue));

            _publicListe = new ObservableCollection<Person>();
        }

        // Get henter data fra databasen og kopierer det over i den lokale kopi
        public ObservableCollection<Person> Get()
        {
            _publicListe.Clear();     //Først tømmes den lokale kopi

            //Så løber vi alle elementerne igennem i databasen og overfører til lokal kopi
            foreach (Person p in DataBase)
            {
                _publicListe.Add(p);
            }

            return _publicListe;
        }

        // Commit indsætter vores lokale kopi af data, i databasen
        public void Commit()
        {
            DataBase = new ObservableCollection<Person>(_publicListe);
        }

        public int Delete(Person person)
        {
            int returværdi = 0;

            for (int i = _publicListe.Count - 1; i >= 0; i--)
            {
                if (_publicListe[i].ID == person.ID)
                {
                    _publicListe.RemoveAt(i);
                    returværdi++;
                }
            }
            Commit();
            return returværdi;
        }

        public int Update(int ID, string Fornavn, string Efternavn, int Formue)
        {
            // Vi laver en returværdi, så hvis vi ikke finder noget, returnerer vi 0
            int returværdi = 0;

            //Løber igennem listen en efter en, for at sammenligne ID
            foreach (Person p in _publicListe)
            {
                if (p.ID == ID)
                {
                    p.Fornavn = Fornavn;
                    p.Efternavn = Efternavn;
                    p.Formue = Formue;
                    returværdi++;
                }
            }
            Commit();
            return returværdi;
        }

        public int Insert(string Fornavn, string Efternavn, int Formue)
        {
            Person Person_Object = new Person();

            Person_Object.ID = _publicListe[_publicListe.Count - 1].ID + 1;
            Person_Object.Fornavn = Fornavn;
            Person_Object.Efternavn = Efternavn;
            Person_Object.Formue = Formue;

            _publicListe.Add(Person_Object);
            Commit();
            
            return (Person_Object.ID);
        }

    }
}
