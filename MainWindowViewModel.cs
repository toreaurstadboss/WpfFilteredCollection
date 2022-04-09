using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace WpfFilteredCollection
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {

        private readonly ICollectionView _usersView;

        public event PropertyChangedEventHandler? PropertyChanged;

        public void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public ICollectionView Users
        {
            get { return _usersView; }
        }

        public MainWindowViewModel()
        {
            IList<User> users = GetUsers();
            _usersView = CollectionViewSource.GetDefaultView(users);
            _usersView.Filter = (object u) => u != null && (u as User)?.Name?.Length <= FilterNameLength;
            FilterNameLength = 6;
        }

        private IList<User> GetUsers()
        {
            return new List<User>
            {
                new User { Name = "Bob", IsAdmin = false },
                new User { Name = "Alice", IsAdmin = false }
            };
        }

        private int _filterNameLength = 0;
        public int FilterNameLength
        {
            get { return _filterNameLength; }
            set
            {
                if (_filterNameLength != value)
                {
                    _filterNameLength = value;
                    RaisePropertyChanged("FilterNameLength");
                    _usersView.Refresh();
                }

            }
        }


    }
}
