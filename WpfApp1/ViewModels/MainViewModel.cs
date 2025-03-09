using HandyControl.Controls;
using HandyControl.Data;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WpfApp1.Commands;
using WpfApp1.Models;

namespace WpfApp1.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public ICommand StartCommand => new RelayCommand(StartProcessAsync);
        public ICommand AddCommand => new RelayCommand(AddUserAsync);

        public ICommand DelCommand => new RelayCommand(DelUserAsync);

        private ObservableCollection<User> _users = new ObservableCollection<User>();
        public ObservableCollection<User> Users
        {
            get => _users;
            set
            {
                _users = value;
                OnPropertyChanged(nameof(Users));
            }
        }

        private int _progress;
        public int Progress
        {
            get => _progress;
            set
            {
                _progress = value;
                OnPropertyChanged(nameof(Progress));
            }
        }

        private bool _isProcessing;
        public bool IsProcessing
        {
            get => _isProcessing;
            set
            {
                _isProcessing = value;
                OnPropertyChanged(nameof(IsProcessing));
            }
        }

        private bool _isIndeterminate;
        public bool IsIndeterminate
        {
            get => _isIndeterminate;
            set
            {
                _isIndeterminate = value;
                OnPropertyChanged(nameof(IsIndeterminate));
            }
        }




        private async Task StartProcessAsync()
        {
            IsProcessing = true;
            Progress = 0;

            // 模拟耗时任务
            for (int i = 0; i <= 100; i++)
            {
                await Task.Delay(50); // 异步等待
                Progress = i;         // 更新进度
            }

            IsProcessing = false;


        }

        private async Task AddUserAsync()
        {
            await Task.Delay(1);

            Users.Add(new User
            {
                UserName = $"用户{Users.Count + 1}",
                Age = Users.Count + 18
            });
            //HandyControl.Controls.MessageBox.Success("添加成功!", "提示");
        }

        private async Task DelUserAsync()
        {
            await Task.Delay(1);
            if (Users.Count > 0)
            {
                Users.RemoveAt(Users.Count - 1);
                //HandyControl.Controls.MessageBox.Success("删除成功!", "提示");
                Growl.Success("文件保存成功！", "SuccessMsg");
            }
            else
            {
                //HandyControl.Controls.MessageBox.Error("无用户可删除!", "错误");
                Growl.Error("无用户可删除！", "SuccessMsg");
            }

        }


        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
