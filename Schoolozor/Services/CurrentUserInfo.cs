using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Schoolozor.Model;

namespace Schoolozor.Services
{
    public class CurrentUserInfo : INotifyPropertyChanged
    {
        private readonly IClientUserService _user;

        public event PropertyChangedEventHandler PropertyChanged;

        public SchoolUser CurrentUser { get; set; } = new SchoolUser();
        public int Counter { get; set; } = 0;
        private void NotifyPropertyChanged()
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("CurrentUser"));
        }

        public CurrentUserInfo(IClientUserService user)
        {
            _user = user;
            _user.UserAuthenticatedEvent += _user_UserAuthenticatedEvent;
        }

        private async void _user_UserAuthenticatedEvent(object sender, UserAuthenticatedArgs e)
        {
            if (!string.IsNullOrEmpty(e.UserId))
            {
                await PopulateUser(e.UserId);
            }
            else
            {
                ClearUser();
            }
        }

        public async Task PopulateUser(string id)
        {
            CurrentUser = await _user.GetUserInfo(id);
            NotifyPropertyChanged();
        }
        public void ClearUser()
        {
            CurrentUser = new SchoolUser();
            Counter = 0;
            NotifyPropertyChanged();
        }
    }
}
