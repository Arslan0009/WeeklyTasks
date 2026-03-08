using System;
using System.Collections.Generic;

class Program
{
    //memory lists
    static List<User> users = new List<User>();
    static List<Patient> patients = new List<Patient>();
    static List<Medicine> medicines = new List<Medicine>();
    static List<CareTask> tasks = new List<CareTask>();
    static List<Attendance> attendance = new List<Attendance>();
    static List<Vitals> vitalsList = new List<Vitals>();

    // Currently logged-in user
    static string currentUser = "";

    // --------------------------------------
    // ENTRY POINT
    // --------------
    static void Main()
    {
        ShowWelcome();
    }

    //  VALIDATION
    
    static bool IsName(string name)
    {
        if (string.IsNullOrWhiteSpace(name)) return false;
        foreach (char c in name)
        {
            if (!char.IsLetter(c) && c != ' ') return false;
        }
        return true;
    }

    //  exactly 11 digits
    static bool IsPhone(string phone)
    {
        if (phone.Length != 11) return false;
        foreach (char c in phone)
        {
            if (!char.IsDigit(c)) return false;
        }
        return true;
    }

    // Must be digits only
    static bool IsNumber(string num)
    {
        if (string.IsNullOrEmpty(num)) return false;
        foreach (char c in num)
        {
            if (!char.IsDigit(c)) return false;
        }
        return true;
    }

    // DD/MM/YYYY format 
    static bool IsValidDate(string date)
    {
        if (date.Length != 10) return false;
        if (date[2] != '/' || date[5] != '/') return false;

        for (int i = 0; i < date.Length; i++)
        {
            if (i == 2 || i == 5) continue;
            if (!char.IsDigit(date[i])) return false;
        }

        int day = int.Parse(date.Substring(0, 2));
        int month = int.Parse(date.Substring(3, 2));
        int year = int.Parse(date.Substring(6, 4));

        if (day < 1 || day > 31) return false;
        if (month < 1 || month > 12) return false;
        if (year < 2000 || year > 2100) return false;

        return true;
    }

    // Get today's date 
    static string GetDate()
    {
        return DateTime.Now.ToString("dd/MM/yyyy");
    }

    // Clear the console screen
    static void ClearScreen()
    {
        Console.Clear();
    }

    
    static void PrintLine()
    {
        Console.WriteLine("========================================");
    }

   // error message
    static void ShowError(string message)
    {
        Console.WriteLine($"\n---\nERROR: {message}\n---");
    }

    // success message
    static void ShowSuccess(string message)
    {
        Console.WriteLine($"\n---\nSUCCESS: {message}\n---");
    }

    // Pause and wait for Enter key
    static void PressEnter()
    {
        Console.WriteLine("\nPress Enter to continue...");
        Console.ReadLine();
    }

    // =========
    //  USER AUTHENTICATION
    // =========

    // Check if a username already exists
    static bool UserExists(string username)
    {
        foreach (User u in users)
        {
            if (u.Username == username) return true;
        }
        return false;
    }

    // Check login credentials
    static bool LoginCheck(string username, string password)
    {
        foreach (User u in users)
        {
            if (u.Username == username && u.Password == password) return true;
        }
        return false;
    }

    // ==============
    //  WELCOME SCREEN
    // ===============
    static void ShowWelcome()
    {
        ClearScreen();
        PrintLine();
        Console.WriteLine("       CARETRACK SYSTEM");
        Console.WriteLine("   Family Caregiver Management");
        PrintLine();

        Console.WriteLine("\n1. Login");
        Console.WriteLine("2. Sign Up");
        Console.WriteLine("0. Exit\n");
        Console.Write("Select Option: ");

        string choice = Console.ReadLine();

        if (choice == "1") ShowLogin();
        else if (choice == "2") ShowSignup();
        else if (choice == "0") { ClearScreen(); Console.WriteLine("\nThank you! Goodbye."); }
        else
        {
            ShowError("Wrong choice! Please select 1, 2, or 0.");
            PressEnter();
            ShowWelcome();
        }
    }

    // ==============
    //  SIGN UP
    // ===============
    static void ShowSignup()
    {
        ClearScreen();
        PrintLine();
        Console.WriteLine("  SIGN UP");
        PrintLine();

        // --- Username ---
        string username;
        while (true)
        {
            Console.Write("\nUsername (min 6 characters) or '0' to go back: ");
            username = Console.ReadLine().Trim();

            if (username == "0") { ShowWelcome(); return; }

            if (username.Length < 6)
                ShowError("Username must be at least 6 characters!");
            else if (UserExists(username))
                ShowError("Username already exists! Try a different one.");
            else
                break;
        }

        // --- Password ---
        string password;
        while (true)
        {
            Console.Write("Password (min 6 characters) or '0' to go back: ");
            password = Console.ReadLine().Trim();

            if (password == "0") { ShowWelcome(); return; }

            if (password.Length < 6)
                ShowError("Password must be at least 6 characters!");
            else
                break;
        }

        // Save the new user
        users.Add(new User(username, password));
        ShowSuccess("Account created! Please login.");
        PressEnter();
        ShowLogin();
    }

    // ============================================================
    //  LOGIN
    // ============================================================
    static void ShowLogin()
    {
        ClearScreen();
        PrintLine();
        Console.WriteLine("    LOGIN");
        PrintLine();

        Console.Write("\nUsername: ");
        string username = Console.ReadLine().Trim();

        Console.Write("Password: ");
        string password = Console.ReadLine().Trim();

        if (LoginCheck(username, password))
        {
            currentUser = username;
            ShowSuccess("Login successful! Welcome, " + username);
            PressEnter();
            ShowMainMenu();
        }
        else
        {
            ShowError("Wrong username or password!");
            Console.WriteLine("\n1. Try again");
            Console.WriteLine("0. Back to Welcome");
            Console.Write("\nSelect Option: ");
            string choice = Console.ReadLine();

            if (choice == "1") ShowLogin();
            else ShowWelcome();
        }
    }

    // =====================
    //  MAIN MENU
    // =====================
    static void ShowMainMenu()
    {
        ClearScreen();
        PrintLine();
        Console.WriteLine(" MAIN MENU");
        PrintLine();
        Console.WriteLine($"  Logged in as: {currentUser}");
        Console.WriteLine("----------------------------------------");
        Console.WriteLine("1. Patient Profile");
        Console.WriteLine("2. Medication Management");
        Console.WriteLine("3. Care Tasks");
        Console.WriteLine("4. Caregiver Attendance");
        Console.WriteLine("5. Vitals & Daily Notes");
        Console.WriteLine("6. Reports");
        Console.WriteLine("7. Emergency Mode");
        Console.WriteLine("8. Logout");
        Console.WriteLine("----------------------------------------");
        Console.Write("\nSelect Option: ");

        string choice = Console.ReadLine();

        if (choice == "1") ShowPatientMenu();
        else if (choice == "2") ShowMedicineMenu();
        else if (choice == "3") ShowTaskMenu();
        else if (choice == "4") ShowAttendanceMenu();
        else if (choice == "5") ShowVitalsMenu();
        else if (choice == "6") ShowReportMenu();
        else if (choice == "7") ShowEmergency();
        else if (choice == "8") Logout();
        else
        {
            ShowError("Wrong choice! Enter a number from 1 to 8.");
            PressEnter();
            ShowMainMenu();
        }
    }

    // ====================
    //  PATIENT SECTION
    // =====================
    static void ShowPatientMenu()
    {
        ClearScreen();
        PrintLine();
        Console.WriteLine("           PATIENT PROFILE");
        PrintLine();
        Console.WriteLine("\n1. Add Patient");
        Console.WriteLine("2. View Patients");
        Console.WriteLine("3. Update Patient");
        Console.WriteLine("0. Back\n");
        Console.Write("Select Option: ");

        string choice = Console.ReadLine();

        if (choice == "1") AddPatient();
        else if (choice == "2") ViewPatients();
        else if (choice == "3") UpdatePatient();
        else if (choice == "0") ShowMainMenu();
        else
        {
            ShowError("Wrong choice!");
            PressEnter();
            ShowPatientMenu();
        }
    }

    static void AddPatient()
    {
        ClearScreen();
        PrintLine();
        Console.WriteLine("             ADD PATIENT");
        PrintLine();

        // Name
        string name;
        while (true)
        {
            Console.Write("\nEnter Patient Name: ");
            name = Console.ReadLine().Trim();
            if (IsName(name)) break;
            ShowError("Name must contain letters only! No numbers or symbols.");
        }

        // Age
        string age;
        while (true)
        {
            Console.Write("Enter Age: ");
            age = Console.ReadLine().Trim();
            if (!IsNumber(age))
            {
                ShowError("Age must be a number!");
                continue;
            }
            int ageVal = int.Parse(age);
            if (ageVal < 1 || ageVal > 120)
            {
                ShowError("Age must be between 1 and 120!");
                continue;
            }
            break;
        }  

        // Condition
        string condition;
        while (true)
        {
            Console.Write("Enter Medical Condition: ");
            condition = Console.ReadLine().Trim();
            if (IsName(condition)) break;
            ShowError("Medical condition must contain letters only!");
        }

        // Allergies
        string allergies;
        while (true)
        {
            Console.Write("Enter Allergies (or type 'None'): ");
            allergies = Console.ReadLine().Trim();
            if (IsName(allergies)) break;
            ShowError("Allergies must contain letters only!");
        }

        // Contact
        string contact;
        while (true)
        {
            Console.Write("Enter Emergency Contact (11 digits): ");
            contact = Console.ReadLine().Trim();
            if (IsPhone(contact)) break;
            ShowError("Phone number must be exactly 11 digits!");
        }

        // Add to list
        int newId = patients.Count + 1;
        patients.Add(new Patient(newId, name, age, condition, allergies, contact));
        ShowSuccess("Patient Added Successfully!");
        PressEnter();
        ShowPatientMenu();
    }

    static void ViewPatients()
    {
        ClearScreen();
        PrintLine();
        Console.WriteLine("            VIEW PATIENTS");
        PrintLine();

        if (patients.Count == 0)
        {
            Console.WriteLine("\n  No patients found.");
        }
        else
        {
            Console.WriteLine();
            foreach (Patient p in patients)
            {
                p.Display();
            }
        }

        PressEnter();
        ShowPatientMenu();
    }

    static void UpdatePatient()
    {
        ClearScreen();
        PrintLine();
        Console.WriteLine("UPDATE PATIENT");
        PrintLine();

        if (patients.Count == 0)
        {
            Console.WriteLine("\n  No patients found.");
            PressEnter();
            ShowPatientMenu();
            return;
        }

        // Show all patients first
        Console.WriteLine();
        foreach (Patient p in patients)
        {
            p.Display();
        }

        // Get ID to update
        Console.Write("\nEnter Patient ID to update: ");
        string idInput = Console.ReadLine().Trim();

        if (!IsNumber(idInput))
        {
            ShowError("ID must be a number!");
            PressEnter();
            ShowPatientMenu();
            return;
        }

        int id = int.Parse(idInput);

        // Find the patient
        Patient found = null;
        foreach (Patient p in patients)
        {
            if (p.Id == id) { found = p; break; }
        }

        if (found == null)
        {
            ShowError("Patient with that ID was not found!");
            PressEnter();
            ShowPatientMenu();
            return;
        }

        
        Console.WriteLine("\nWhat do you want to update?");
        Console.WriteLine("1. Name");
        Console.WriteLine("2. Age");
        Console.WriteLine("3. Condition");
        Console.WriteLine("4. Allergies");
        Console.WriteLine("5. Contact");
        Console.Write("\nChoice: ");
        string choice = Console.ReadLine();

        if (choice == "1")
        {
            while (true)
            {
                Console.Write("New Name: ");
                string val = Console.ReadLine().Trim();
                if (IsName(val)) { found.Name = val; break; }
                ShowError("Name must contain letters only!");
            }
        }
        else if (choice == "2")
        {
            while (true)
            {
                Console.Write("New Age: ");
                string val = Console.ReadLine().Trim();
                if (!IsNumber(val)) { ShowError("Age must be a number!"); continue; }
                int ageVal = int.Parse(val);
                if (ageVal < 1 || ageVal > 120) { ShowError("Age must be between 1 and 120!"); continue; }
                found.Age = val;
                break;
            }
        }
        else if (choice == "3")
        {
            while (true)
            {
                Console.Write("New Condition: ");
                string val = Console.ReadLine().Trim();
                if (IsName(val)) { found.Condition = val; break; }
                ShowError("Condition must contain letters only!");
            }
        }
        else if (choice == "4")
        {
            while (true)
            {
                Console.Write("New Allergies: ");
                string val = Console.ReadLine().Trim();
                if (IsName(val)) { found.Allergies = val; break; }
                ShowError("Allergies must contain letters only!");
            }
        }
        else if (choice == "5")
        {
            while (true)
            {
                Console.Write("New Contact (11 digits): ");
                string val = Console.ReadLine().Trim();
                if (IsPhone(val)) { found.Contact = val; break; }
                ShowError("Phone must be exactly 11 digits!");
            }
        }
        else
        {
            ShowError("Wrong choice! No changes made.");
            PressEnter();
            ShowPatientMenu();
            return;
        }

        ShowSuccess("Patient updated successfully!");
        PressEnter();
        ShowPatientMenu();
    }

    // ================
    //  MEDICINE SECTION
    // =================
    static void ShowMedicineMenu()
    {
        ClearScreen();
        PrintLine();
        Console.WriteLine("       MEDICATION MANAGEMENT");
        PrintLine();
        Console.WriteLine("\n1. Add Medicine");
        Console.WriteLine("2. View Medicines");
        Console.WriteLine("3. Mark Medicine Taken/Missed");
        Console.WriteLine("0. Back\n");
        Console.Write("Select Option: ");

        string choice = Console.ReadLine();

        if (choice == "1") AddMedicine();
        else if (choice == "2") ViewMedicines();
        else if (choice == "3") MarkMedicine();
        else if (choice == "0") ShowMainMenu();
        else
        {
            ShowError("Wrong choice!");
            PressEnter();
            ShowMedicineMenu();
        }
    }

    static void AddMedicine()
    {
        ClearScreen();
        PrintLine();
        Console.WriteLine(" ADD MEDICINE");
        PrintLine();

        // Name
        string name;
        while (true)
        {
            Console.Write("\nMedicine Name: ");
            name = Console.ReadLine().Trim();
            if (IsName(name)) break;
            ShowError("Medicine name must contain letters only!");
        }

        // Dosage
        string dosage;
        while (true)
        {
            Console.Write("Dosage (mg): ");
            dosage = Console.ReadLine().Trim();
            if (IsNumber(dosage)) break;
            ShowError("Dosage must be a number!");
        }

        // Times per day
        string times;
        while (true)
        {
            Console.Write("Times Per Day: ");
            times = Console.ReadLine().Trim();
            if (!IsNumber(times)) { ShowError("Times per day must be a number!"); continue; }
            if (int.Parse(times) < 1 || int.Parse(times) > 10) { ShowError("Times per day must be between 1 and 10!"); continue; }
            break;
        }

        int newId = medicines.Count + 1;
        medicines.Add(new Medicine(newId, name, dosage, times));
        ShowSuccess("Medicine Added Successfully! Status set to Pending.");
        PressEnter();
        ShowMedicineMenu();
    }

    static void ViewMedicines()
    {
        ClearScreen();
        PrintLine();
        Console.WriteLine("          VIEW MEDICINES");
        PrintLine();

        if (medicines.Count == 0)
        {
            Console.WriteLine("\n  No medicines found.");
        }
        else
        {
            Console.WriteLine();
            foreach (Medicine m in medicines)
            {
                m.Display();
            }
        }

        PressEnter();
        ShowMedicineMenu();
    }

    static void MarkMedicine()
    {
        ClearScreen();
        PrintLine();
        Console.WriteLine("      MARK MEDICINE STATUS");
        PrintLine();

        if (medicines.Count == 0)
        {
            Console.WriteLine("\n  No medicines found.");
            PressEnter();
            ShowMedicineMenu();
            return;
        }

        Console.WriteLine();
        foreach (Medicine m in medicines)
        {
            m.Display();
        }

        Console.Write("\nEnter Medicine ID: ");
        string idInput = Console.ReadLine().Trim();

        if (!IsNumber(idInput))
        {
            ShowError("ID must be a number!");
            PressEnter();
            ShowMedicineMenu();
            return;
        }

        int id = int.Parse(idInput);

        Medicine found = null;
        foreach (Medicine m in medicines)
        {
            if (m.Id == id) { found = m; break; }
        }

        if (found == null)
        {
            ShowError("Medicine with that ID was not found!");
            PressEnter();
            ShowMedicineMenu();
            return;
        }

        // Only allow marking if still Pending
        if (found.Status != "Pending")
        {
            ShowError($"This medicine is already marked as '{found.Status}'. Cannot change again!");
            PressEnter();
            ShowMedicineMenu();
            return;
        }

        Console.WriteLine("\n1. Taken");
        Console.WriteLine("2. Missed");
        Console.Write("\nSelect Option: ");
        string choice = Console.ReadLine();

        if (choice == "1") { found.Status = "Taken"; ShowSuccess("Medicine marked as Taken!"); }
        else if (choice == "2") { found.Status = "Missed"; ShowSuccess("Medicine marked as Missed!"); }
        else { ShowError("Wrong choice! Status not changed."); }

        PressEnter();
        ShowMedicineMenu();
    }

    // =============
    //  TASK SECTION
    // =============
    static void ShowTaskMenu()
    {
        ClearScreen();
        PrintLine();
        Console.WriteLine("            CARE TASKS");
        PrintLine();
        Console.WriteLine("\n1. Add Task");
        Console.WriteLine("2. View Tasks");
        Console.WriteLine("3. Mark Task Completed");
        Console.WriteLine("0. Back\n");
        Console.Write("Select Option: ");

        string choice = Console.ReadLine();

        if (choice == "1") AddTask();
        else if (choice == "2") ViewTasks();
        else if (choice == "3") MarkTaskDone();
        else if (choice == "0") ShowMainMenu();
        else
        {
            ShowError("Wrong choice!");
            PressEnter();
            ShowTaskMenu();
        }
    }

    static void AddTask()
    {
        ClearScreen();
        PrintLine();
        Console.WriteLine("            ADD TASK");
        PrintLine();

        // Title
        string title;
        while (true)
        {
            Console.Write("\nTask Title: ");
            title = Console.ReadLine().Trim();
            if (!string.IsNullOrWhiteSpace(title)) break;
            ShowError("Task title cannot be empty!");
        }

        // Assigned to
        string person;
        while (true)
        {
            Console.Write("Assigned To: ");
            person = Console.ReadLine().Trim();
            if (IsName(person)) break;
            ShowError("Name must contain letters only!");
        }

        // Due date
        string date;
        while (true)
        {
            Console.Write("Due Date (DD/MM/YYYY): ");
            date = Console.ReadLine().Trim();
            if (IsValidDate(date)) break;
            ShowError("Date must be in DD/MM/YYYY format with valid values!");
        }

        int newId = tasks.Count + 1;
        tasks.Add(new CareTask(newId, title, person, date));
        ShowSuccess("Task Added Successfully! Status set to Pending.");
        PressEnter();
        ShowTaskMenu();
    }

    static void ViewTasks()
    {
        ClearScreen();
        PrintLine();
        Console.WriteLine("VIEW TASKS");
        PrintLine();

        if (tasks.Count == 0)
        {
            Console.WriteLine("\n  No tasks found.");
        }
        else
        {
            Console.WriteLine();
            foreach (CareTask t in tasks)
            {
                t.Display();
            }
        }

        PressEnter();
        ShowTaskMenu();
    }

    static void MarkTaskDone()
    {
        ClearScreen();
        PrintLine();
        Console.WriteLine("       MARK TASK COMPLETED");
        PrintLine();

        if (tasks.Count == 0)
        {
            Console.WriteLine("\n  No tasks found.");
            PressEnter();
            ShowTaskMenu();
            return;
        }

        Console.WriteLine();
        foreach (CareTask t in tasks)
        {
            t.Display();
        }

        Console.Write("\nEnter Task ID to mark as completed: ");
        string idInput = Console.ReadLine().Trim();

        if (!IsNumber(idInput))
        {
            ShowError("ID must be a number!");
            PressEnter();
            ShowTaskMenu();
            return;
        }

        int id = int.Parse(idInput);

        CareTask found = null;
        foreach (CareTask t in tasks)
        {
            if (t.Id == id) { found = t; break; }
        }

        if (found == null)
        {
            ShowError("Task with that ID was not found!");
        }
        else if (found.Status == "Completed")
        {
            ShowError("This task is already marked as Completed!");
        }
        else
        {
            found.Status = "Completed";
            ShowSuccess("Task marked as Completed!");
        }

        PressEnter();
        ShowTaskMenu();
    }

    // ============================================================
    //  ATTENDANCE SECTION
    // ============================================================
    static void ShowAttendanceMenu()
    {
        ClearScreen();
        PrintLine();
        Console.WriteLine("       CAREGIVER ATTENDANCE");
        PrintLine();
        Console.WriteLine("\n1. Check In");
        Console.WriteLine("2. Check Out");
        Console.WriteLine("3. View Attendance");
        Console.WriteLine("0. Back\n");
        Console.Write("Select Option: ");

        string choice = Console.ReadLine();

        if (choice == "1") CheckIn();
        else if (choice == "2") CheckOut();
        else if (choice == "3") ViewAttendance();
        else if (choice == "0") ShowMainMenu();
        else
        {
            ShowError("Wrong choice!");
            PressEnter();
            ShowAttendanceMenu();
        }
    }

    static void CheckIn()
    {
        ClearScreen();
        PrintLine();
        Console.WriteLine("             CHECK IN");
        PrintLine();

        string name;
        while (true)
        {
            Console.Write("\nEnter Caregiver Name: ");
            name = Console.ReadLine().Trim();
            if (IsName(name)) break;
            ShowError("Name must contain letters only!");
        }

        attendance.Add(new Attendance(GetDate(), name, "Check-In"));
        ShowSuccess("Checked In successfully!");
        PressEnter();
        ShowAttendanceMenu();
    }

    static void CheckOut()
    {
        ClearScreen();
        PrintLine();
        Console.WriteLine("             CHECK OUT");
        PrintLine();

        string name;
        while (true)
        {
            Console.Write("\nEnter Careegiver Name: ");
            name = Console.ReadLine().Trim();
            if (IsName(name)) break;
            ShowError("Name must contain letters only!");
        }

        attendance.Add(new Attendance(GetDate(), name, "Check-Out"));
        ShowSuccess("Checked Out successfully!");
        PressEnter();
        ShowAttendanceMenu();
    }

    static void ViewAttendance()
    {
        ClearScreen();
        PrintLine();
        Console.WriteLine("        VIEW ATTENDANCE");
        PrintLine();

        if (attendance.Count == 0)
        {
            Console.WriteLine("\n  No attendance records found.");
        }
        else
        {
            Console.WriteLine();
            foreach (Attendance a in attendance)
            {
                a.Display();
            }
        }

        PressEnter();
        ShowAttendanceMenu();
    }

    // ============================================================
    //  VITALS & DAILY NOTES SECTION
    // ============================================================
    static void ShowVitalsMenu()
    {
        ClearScreen();
        PrintLine();
        Console.WriteLine("       VITALS & DAILY NOTES");
        PrintLine();
        Console.WriteLine("\n1. Add Vitals");
        Console.WriteLine("2. Add Daily Note");
        Console.WriteLine("3. View Vitals History");
        Console.WriteLine("0. Back\n");
        Console.Write("Select Option: ");

        string choice = Console.ReadLine();

        if (choice == "1") AddVitals();
        else if (choice == "2") AddNote();
        else if (choice == "3") ViewVitals();
        else if (choice == "0") ShowMainMenu();
        else
        {
            ShowError("Wrong choice!");
            PressEnter();
            ShowVitalsMenu();
        }
    }

    static void AddVitals()
    {
        ClearScreen();
        PrintLine();
        Console.WriteLine("            ADD VITALS");
        PrintLine();

        // Blood Pressure
        string bp;
        while (true)
        {
            Console.Write("\nBlood Pressure (e.g. 120): ");
            bp = Console.ReadLine().Trim();
            if (IsNumber(bp)) break;
            ShowError("Blood pressure must be a number!");
        }

        // Sugar Level
        string sugar;
        while (true)
        {
            Console.Write("Sugar Level (e.g. 90): ");
            sugar = Console.ReadLine().Trim();
            if (IsNumber(sugar)) break;
            ShowError("Sugar level must be a number!");
        }

        // Temperature
        string temp;
        while (true)
        {
            Console.Write("Temperature (e.g. 37): ");
            temp = Console.ReadLine().Trim();
            if (IsNumber(temp)) break;
            ShowError("Temperature must be a number!");
        }

        vitalsList.Add(new Vitals(bp, sugar, temp, ""));
        ShowSuccess("Vitals Recorded!");

        Console.WriteLine($"\n  Blood Pressure : {bp}");
        Console.WriteLine($"  Sugar Level    : {sugar}");
        Console.WriteLine($"  Temperature    : {temp}");

        PressEnter();
        ShowVitalsMenu();
    }

    static void AddNote()
    {
        ClearScreen();
        PrintLine();
        Console.WriteLine("        ADD DAILY NOTE");
        PrintLine();

        string note;
        while (true)
        {
            Console.Write("\nEnter Daily Note: ");
            note = Console.ReadLine().Trim();
            if (!string.IsNullOrWhiteSpace(note)) break;
            ShowError("Note cannot be empty!");
        }

        // Save note to last 
        if (vitalsList.Count > 0)
        {
            vitalsList[vitalsList.Count - 1].Note = note;
        }
        else
        {
            vitalsList.Add(new Vitals("N/A", "N/A", "N/A", note));
        }

        ShowSuccess("Daily note saved!");
        PressEnter();
        ShowVitalsMenu();
    }

    static void ViewVitals()
    {
        ClearScreen();
        PrintLine();
        Console.WriteLine("        VIEW VITALS HISTORY");
        PrintLine();

        if (vitalsList.Count == 0)
        {
            Console.WriteLine("\n  No vitals records found.");
        }
        else
        {
            Console.WriteLine();
            foreach (Vitals v in vitalsList)
            {
                v.Display();
            }
        }

        PressEnter();
        ShowVitalsMenu();
    }

    // ============================================================
    //  REPORTS SECTION
    // ============================================================
    static void ShowReportMenu()
    {
        ClearScreen();
        PrintLine();
        Console.WriteLine("              REPORTS");
        PrintLine();
        Console.WriteLine("\n1. Daily Summary");
        Console.WriteLine("2. Missed Medicines Report");
        Console.WriteLine("3. Attendance Report");
        Console.WriteLine("0. Back\n");
        Console.Write("Select Option: ");

        string choice = Console.ReadLine();

        if (choice == "1") ShowDailyReport();
        else if (choice == "2") ShowMissedReport();
        else if (choice == "3") ShowAttendanceReport();
        else if (choice == "0") ShowMainMenu();
        else
        {
            ShowError("Wrong choice!");
            PressEnter();
            ShowReportMenu();
        }
    }

    static void ShowDailyReport()
    {
        ClearScreen();
        PrintLine();
        Console.WriteLine("          DAILY SUMMARY");
        PrintLine();

        // Count pending medicines
        int pendingMedicines = 0;
        foreach (Medicine m in medicines)
        {
            if (m.Status == "Pending") pendingMedicines++;
        }

        // Count pending tasks
        int pendingTasks = 0;
        foreach (CareTask t in tasks)
        {
            if (t.Status == "Pending") pendingTasks++;
        }

        Console.WriteLine($"\n  Date               : {GetDate()}");
        Console.WriteLine($"  Total Patients     : {patients.Count}");
        Console.WriteLine($"  Total Medicines    : {medicines.Count}");
        Console.WriteLine($"  Medicines Pending  : {pendingMedicines}");
        Console.WriteLine($"  Total Tasks        : {tasks.Count}");
        Console.WriteLine($"  Tasks Pending      : {pendingTasks}");
        Console.WriteLine($"  Attendance Records : {attendance.Count}");

        PressEnter();
        ShowReportMenu();
    }

    static void ShowMissedReport()
    {
        ClearScreen();
        PrintLine();
        Console.WriteLine("    MISSED MEDICINES REPORT");
        PrintLine();

        int count = 0;
        Console.WriteLine();
        foreach (Medicine m in medicines)
        {
            if (m.Status == "Missed")
            {
                m.Display();
                count++;
            }
        }

        if (count == 0)
            Console.WriteLine("  No missed medicines. Great job!");

        PressEnter();
        ShowReportMenu();
    }

    static void ShowAttendanceReport()
    {
        ClearScreen();
        PrintLine();
        Console.WriteLine("        ATTENDANCE REPORT");
        PrintLine();

        int checkIns = 0, checkOuts = 0;

        foreach (Attendance a in attendance)
        {
            if (a.Type == "Check-In") checkIns++;
            if (a.Type == "Check-Out") checkOuts++;
        }

        Console.WriteLine($"\n  Check-Ins  : {checkIns}");
        Console.WriteLine($"  Check-Outs : {checkOuts}");

        if (checkIns == 0 && checkOuts == 0)
            Console.WriteLine("\n  No attendance records found.");

        PressEnter();
        ShowReportMenu();
    }

    // ===============
    //  EMERGENCY MODE
    // ================
    static void ShowEmergency()
    {
        ClearScreen();
        PrintLine();
        Console.WriteLine("          EMERGENCY MODE");
        PrintLine();

        Console.Write("\nEnter Emergency Reason: ");
        string reason = Console.ReadLine().Trim();

        if (string.IsNullOrWhiteSpace(reason))
        {
            ShowError("Emergency reason cannot be empty!");
            PressEnter();
            ShowMainMenu();
            return;
        }

        // Get first patient's emergency contact if available
        string contact = "Not available";
        if (patients.Count > 0)
            contact = patients[0].Contact;

        Console.WriteLine($"\n  Reason  : {reason}");
        Console.WriteLine($"  Contact : {contact}");
        Console.WriteLine("\n  *** Please call the emergency contact immediately! ***");

        PressEnter();
        ShowMainMenu();
    }

    // ============================================================
    //  LOGOUT
    // ============================================================
    static void Logout()
    {
        ClearScreen();
        PrintLine();
        Console.WriteLine("              LOGOUT");
        PrintLine();

        Console.WriteLine("\nAre you sure you want to logout?");
        Console.WriteLine("1. Yes");
        Console.WriteLine("0. No\n");
        Console.Write("Select Option: ");

        string choice = Console.ReadLine();

        if (choice == "1")
        {
            currentUser = "";
            ShowSuccess("Logged out successfully!");
            PressEnter();
            ShowWelcome();
        }
        else
        {
            ShowMainMenu();
        }
    }
}



// -----------------------------------------------
// USER CLASS
// -----------------------------------------------
class User
{
    public string Username;
    public string Password;

    public User(string username, string password)
    {
        Username = username;
        Password = password;
    }
}

// -----------------------------------------------
// PATIENT CLASS
// -----------------------------------------------
class Patient
{
    public int Id;
    public string Name;
    public string Age;
    public string Condition;
    public string Allergies;
    public string Contact;

    public Patient(int id, string name, string age, string condition, string allergies, string contact)
    {
        Id = id;
        Name = name;
        Age = age;
        Condition = condition;
        Allergies = allergies;
        Contact = contact;
    }

    
    public void Display()
    {
        Console.WriteLine($"  ID        : {Id}");
        Console.WriteLine($"  Name      : {Name}");
        Console.WriteLine($"  Age       : {Age}");
        Console.WriteLine($"  Condition : {Condition}");
        Console.WriteLine($"  Allergies : {Allergies}");
        Console.WriteLine($"  Contact   : {Contact}");
        Console.WriteLine("  ----------------------------------");
    }
}

// -----------------------------------------------
// MEDICINE CLASS
// -----------------------------------------------
class Medicine
{
    public int Id;
    public string Name;
    public string Dosage;
    public string TimesPerDay;
    public string Status;   // Pending / Taken / Missed

    public Medicine(int id, string name, string dosage, string timesPerDay)
    {
        Id = id;
        Name = name;
        Dosage = dosage;
        TimesPerDay = timesPerDay;
        Status = "Pending";   // Every new medicine starts as Pending
    }

    public void Display()
    {
        Console.WriteLine($"  ID          : {Id}");
        Console.WriteLine($"  Name        : {Name}");
        Console.WriteLine($"  Dosage      : {Dosage} mg");
        Console.WriteLine($"  Times/Day   : {TimesPerDay}");
        Console.WriteLine($"  Status      : {Status}");   // Pending / Taken / Missed
        Console.WriteLine("  ----------------------------------");
    }
}

// -----------------------------------------------
// TASK CLASS
// -----------------------------------------------
class CareTask
{
    public int Id;
    public string Title;
    public string AssignedTo;
    public string DueDate;
    public string Status;   // Pending / Completed

    public CareTask(int id, string title, string assignedTo, string dueDate)
    {
        Id = id;
        Title = title;
        AssignedTo = assignedTo;
        DueDate = dueDate;
        Status = "Pending";   // Every new task starts as Pending
    }

    public void Display()
    {
        Console.WriteLine($"  ID          : {Id}");
        Console.WriteLine($"  Title       : {Title}");
        Console.WriteLine($"  Assigned To : {AssignedTo}");
        Console.WriteLine($"  Due Date    : {DueDate}");
        Console.WriteLine($"  Status      : {Status}");   // Pending / Completed
        Console.WriteLine("  ----------------------------------");
    }
}

// -----------------------------------------------
// ATTENDANCE CLASS
// -----------------------------------------------
class Attendance
{
    public string Date;
    public string CaregiverName;
    public string Type;   // Check-In / Check-Out

    public Attendance(string date, string caregiverName, string type)
    {
        Date = date;
        CaregiverName = caregiverName;
        Type = type;
    }

    public void Display()
    {
        Console.WriteLine($"  Date      : {Date}");
        Console.WriteLine($"  Caregiver : {CaregiverName}");
        Console.WriteLine($"  Type      : {Type}");
        Console.WriteLine("  ----------------------------------");
    }
}

// -----------------------------------------------
// VITALS CLASS
// -----------------------------------------------
class Vitals
{
    public string BloodPressure;
    public string SugarLevel;
    public string Temperature;
    public string Note;

    public Vitals(string bp, string sugar, string temp, string note)
    {
        BloodPressure = bp;
        SugarLevel = sugar;
        Temperature = temp;
        Note = note;
    }

    public void Display()
    {
        Console.WriteLine($"  Blood Pressure : {BloodPressure}");
        Console.WriteLine($"  Sugar Level    : {SugarLevel}");
        Console.WriteLine($"  Temperature    : {Temperature}");
        Console.WriteLine($"  Note           : {(string.IsNullOrEmpty(Note) ? "No note added" : Note)}");
        Console.WriteLine("  ----------------------------------");
    }
}