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

            //frmMain main = NinjectManager
            //    .GetInstance()
            //    .GetInjectedInstance<frmMain>();
            frmMain main = new frmMain();

            PrepareControllers(main);

            Application.Run(main);
        }

        static void PrepareControllers(frmMain main)
        {
            IEFRepository repository  = new EFRepository();
            IControllerManager manager = ControllerManager.GetInstance();

            manager.RegisterController(new LogEntriesController(repository, main));
            manager.RegisterController(new SummaryLogsController(repository, new frmSummarizeLogs()));
            manager.RegisterController(new PersonalNoteController(repository, new frmPersonalNotes()));
            manager.RegisterController(new ObjectiveController(repository, new frmObjectives()));
            manager.RegisterController(new LeaveController(repository, new frmLeaves()));
            manager.RegisterController(new DailyAttributeController(repository, new frmDailyAttribute()));
            manager.RegisterController(new HolidayController(repository, new frmHolidays()));
            manager.RegisterController(new DailyActivityController(repository, new frmDailyActivity()));
            manager.RegisterController(new CategoryController(repository, new frmCategory()));
            manager.RegisterController(new AttributeController(repository, new frmAttribute()));
            manager.RegisterController(new ActivityController(repository, new frmActivity()));
            manager.RegisterController(new StandardOperatingProcedureController(repository, new frmStandardOperatingProcedure()));

            //Prepare data for main window before showing
            ((LogEntriesController)manager.GetControllerFromId(LogEntriesController.cID)).View.View_ViewReady(null);
        }
    }
}
