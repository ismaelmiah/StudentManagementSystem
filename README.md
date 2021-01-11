# StudentManagementSystem
This application was made as simple assignment task on C# language understanding. Assignmnet Description as follows

## An university student has the follow information: 
- First Name - regular string 
- Middle Name - regular string 
- Last Name - regular string 
- Student ID - regular string in the format XXX-XXX-XXX 
- Joining Batch - a reference to the semester the student joined the university. See semester information below. 
- Department - this will be an enum of {ComputerScience, BBA, English}.
- Degree - this will be an enum of {BSC, BBA, BA, MSC, MBA, MA}. 
- Semesters Attended - a list of semesters. See below for more information. 
- Courses in each Semester - a list of courses per semester attended. See below for more information.

### A semester has the following information: 
- Semester Code - regular string in the format “XXX”, whose possible values are {Summer, Fall, Spring} 
- Year - a regular string in the format “YYYY”, e.g. “2001”. 

### A course has the following information: 
- Course ID - a regular string in the format “XXX YYY”, e.g. CSC 101.
- Course Name - regular string 
- Instructor Name - regular string 
- Number of Credits - integer 

#### You don’t have to save anything in the database. Instead, please save data in JSON files. Store data as JSON string in files. Use Json.Net package and any other package to need from NuGet to convert object to JSON or JSON to object.

## The workflow will be as follows: 
1. In the first screen, we will see the list of students. Also, we will see three options: 
    1. Add New Student
    2. View Student Details
    3. Delete Student.
2. In the Add Student mode, the user needs to enter the following information only: 
    - First Name 
    - Middle Name 
    - Last Name 
    - Student ID 
    - Joining Batch - pre-populated based on the current date. 
    - Department 
    - Degree 

3. In the view student mode, the user can see specific student’s details providing student ID and also show 2 menu. 
   1. Add New Semester mode
   2. Go to menu
4. Delete Student will delete the student and corresponding dependencies. 
5. In the Add New Semester mode, the user can add a new semester to the student and add courses to him/her. 

So maybe we first show courses he/she hasn’t taken yet, and then select from those courses. 
