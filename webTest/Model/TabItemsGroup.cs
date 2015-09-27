using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Windows;

namespace webTest.Model
{
    [Serializable()]
    public class TabItemsGroup : INotifyPropertyChanged
    {
        private string name;
        private ObservableCollection<TabItem> tabItems;
        private int _selectedTabIndex;
        private Visibility _isEditing;

        /// <summary>
        /// Initializes a new instance of the TabItemsGroup class.
        /// </summary>
        public TabItemsGroup()
        {
            name = "default";
            tabItems = new ObservableCollection<TabItem> { new TabItem() };
            SelectedTabIndex = 0;
            IsEditing = Visibility.Hidden;
        }

        /// <summary>
        /// group's Name
        /// </summary>
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                if (name == value)
                    return;

                name = value;
                RaisePropertyChanged("Name");
            }
        }


        public Visibility IsEditing
        {
            get
            {
                return _isEditing;
            }
            set
            {
                if (_isEditing == value)
                    return;

                _isEditing = value;
                RaisePropertyChanged("IsEditing");
            }
        }

        /// <summary>
        /// the selected tab item index
        /// </summary>
        public int SelectedTabIndex
        {
            get
            {
                return _selectedTabIndex;
            }
            set
            {
                if (_selectedTabIndex == value)
                    return;

                _selectedTabIndex = value;
                RaisePropertyChanged("SelectedTabIndex");
                RaisePropertyChanged("CurrentItem");
            }
        }

        /// <summary>
        /// the items in current tabItemsGroup
        /// </summary>
        public ObservableCollection<TabItem> TabItems
        {
            get
            {
                return tabItems;
            }
            set
            {
                if (value == tabItems)
                    return;

                tabItems = value;
                RaisePropertyChanged("TabItems");
            }
        }

        /// <summary>
        /// current item
        /// </summary>
        public TabItem CurrentItem
        {
            get
            {
                if (TabItems != null)
                {
                    if (SelectedTabIndex < 0)
                    {
                        return TabItems[0];
                    }
                    return TabItems[SelectedTabIndex];
                }
                else
                {
                    Console.WriteLine("got null CurrentItem for key {0}", SelectedTabIndex);
                    return null;
                }
            }
            set
            {
                if (TabItems[SelectedTabIndex] == value)
                    return;

                TabItems[SelectedTabIndex] = value;
                RaisePropertyChanged("CurrentItem");
            }
        }

        /// <summary>
        /// method to determine whether the current Item could be delete
        /// </summary>
        public bool CanRemove
        {
            get
            {
                if (SelectedTabIndex == TabItems.Count - 1)
                {
                    return false;
                }
                return true;
            }
        }

        /// <summary>
        /// remove a tabItem by index
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public bool RemoveTabItem(int index)
        {
            if (index == TabItems.Count - 1)
            {
                Console.WriteLine("attemt to delete default item. false return.");
                return false;
            }

            TabItems.RemoveAt(index);
            return true;
        }

        /// <summary>
        /// add a new tabItem.
        /// </summary>
        public void NewTab()
        {
            TabItems.Add(new TabItem());
        }

        #region INotifyPropertyChanged Members

        [field: NonSerialized]
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region INotifyPropertyChanged Methods

        private void RaisePropertyChanged(string propertyName)
        {
            // take a copy to prevent thread issues
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion

    }
}
