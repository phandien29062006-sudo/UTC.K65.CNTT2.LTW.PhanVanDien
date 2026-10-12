//using Day01_lab.bai1;
//StudentService service = new StudentService();
//StudentConsoleview view = new StudentConsoleview();
//MenuManager menu = new MenuManager(service, view);
//menu.Run();


using Day01_lab.Bai2;
EmployeeService service = new EmployeeService();
EmployeeConsoleView view = new EmployeeConsoleView();
EmployeeMenuManager menu = new EmployeeMenuManager(service, view);
menu.Run();