using DevExpress.Mobile.DataGrid;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using XamarinJson.Controls;
using XamarinJson.Helpers;
using XamarinJson.Models;
using XamarinJson.Services;

namespace XamarinJson.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private ObservableRangeCollection<Employee> _employeeList = new ObservableRangeCollection<Employee>();
        public ICommand SearchCommand { get; }
        public ICommand DeleteCommand { get; }
        public ICommand ListViewTappedCommand { get; }

        public ICommand GridRowDoubleTapCommand { get; }

        public ICommand EntryCompletedCommand { get; }

        private Employee _employee = new Employee();

        private string _routeCode = string.Empty;

        private Employee _selectedEmployee;

        public MainViewModel()
        {
            SearchCommand = new Command(async () => await Search());
            DeleteCommand = new Command(async () => await DeleteAsync());
            ListViewTappedCommand = new Command<SelectedItemChangedEventArgs>((obj) => ListViewTapped(obj));
            EntryCompletedCommand = new Command<Entry>((obj) => EntryRouteCode(obj));

            GridRowDoubleTapCommand = new Command<DevExpress.Mobile.DataGrid.RowDoubleTapEventArgs>((e) => GridDoubleRowTap(e));
        }

        private void GridDoubleRowTap(RowDoubleTapEventArgs e)
        {
            Debug.WriteLine(e.RowHandle);
            Debug.WriteLine(e.FieldName);

            Debug.WriteLine(SelectedEmployee.Ename);
        }

        private void EntryRouteCode(object obj)
        {
            Debug.WriteLine("test");

            Debug.WriteLine(RouteCode);

            Debug.WriteLine(((Entry)obj).Text);
            
        }

        private void ListViewTapped(SelectedItemChangedEventArgs obj)
        {
            _employee = obj.SelectedItem as Employee;            
        }

        private async Task DeleteAsync()
        {
            if(_employee != null)
            {
                await ResourceService.GetInstance().DeleteResouce<Employee>(_employee.Empno);
            }
        }

        private async Task Search()
        {
            string responseResult = string.Empty;
            string requestParamJson = string.Empty;
            
            EmployeeList.Clear();
            EmployeeList.AddRange(await ResourceService.GetInstance().GetResources<Employee>(), System.Collections.Specialized.NotifyCollectionChangedAction.Reset);
        }


        public string RouteCode { get => _routeCode; set => SetProperty(ref _routeCode, value); }

        public Employee SelectedEmployee { get => _selectedEmployee; set => SetProperty(ref _selectedEmployee, value); }

        public ObservableRangeCollection<Employee> EmployeeList { get => _employeeList; set => SetProperty(ref this._employeeList, value); }
    }
}
