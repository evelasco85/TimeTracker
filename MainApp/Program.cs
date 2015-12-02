using Domain.Controllers;
using Domain.Infrastructure;
using ModelViewPresenter.MessageDispatcher;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MainApp
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);

                frmMain main = NinjectManager
                    .GetInstance()
                    .GetInjectedInstance<frmMain>();

                Application.Run(main);
        }

        static void T()
        {
            IEFRepository repository  = new EFRepository();
            IControllerManager manager = ControllerManager.GetInstance();

            LogEntriesController controller = new LogEntriesController(repository, new frmMain(repository));
            manager.RegisterController(controller);

            SummaryLogsController controller2 = new SummaryLogsController(repository, new frmSummarizeLogs());
            manager.RegisterController(controller2);
            //controller2.View.View_ViewReady(new { selectedMonth = DateTime.Now });

            PersonalNoteController controller3 = new PersonalNoteController(repository, new frmPersonalNotes());
            manager.RegisterController(controller3);
            //controller3.View.View_ViewReady(null);

            ObjectiveController controller4 = new ObjectiveController(repository, new frmObjectives());
            manager.RegisterController(controller4);
            //controller4.View.View_ViewReady(null);

            LeaveController controller5 = new LeaveController(repository, new frmLeaves());
            manager.RegisterController(controller5);
            //controller5.View.View_ViewReady(null);

            DailyAttributeController controller6 = new DailyAttributeController(repository, new frmDailyAttribute());
            manager.RegisterController(controller6);
            //controller6.View.View_ViewReady(null);

            HolidayController controller7 = new HolidayController(repository, new frmHolidays());
            manager.RegisterController(controller7);
            //controller7.View.View_ViewReady(null);

            DailyActivityController controller8 = new DailyActivityController(repository, new frmDailyActivity());
            manager.RegisterController(controller8);
            //controller8.View.View_ViewReady(null);

            CategoryController controller9 = new CategoryController(repository, new frmCategory());
            manager.RegisterController(controller9);
            //controller9.View.View_ViewReady(null);

            AttributeController controller10 = new AttributeController(repository, new frmAttribute());
            manager.RegisterController(controller10);
            //controller10.View.View_ViewReady(null);

            ActivityController controller11 = new ActivityController(repository, new frmActivity());
            manager.RegisterController(controller11);
            //controller11.View.View_ViewReady(null);
        }
    }
}
