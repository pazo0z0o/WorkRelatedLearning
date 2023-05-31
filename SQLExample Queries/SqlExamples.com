3. From the following table, write a SQL query to find those employees 
whose salary matches the lowest salary of any of the departments. 
Return first name, last name and department ID.   

Select firstname,lastname,department_id
from employees
where Department_Id IN 
(
Select department_id from employees where salary = 

(Select min(salary) from employees) 
)

4. From the following table, write a SQL query to find those employees 
who earn more than the average salary. 
Return employee ID, first name, last name

SELECT first_name, last_name, salary, department_id  
FROM employees  
WHERE salary IN  
( SELECT MIN(salary)  
FROM employees  
GROUP BY department_id 
);

5. 
From the following table, write a SQL query to find those employees 
who report to that manager whose 
first name is ‘Payam’. --this is an equal manager_id 
Return first name, last name, employee ID and salary

6.From the following tables, write a SQL query to find all those employees 
who work in the Finance department.
Return department ID, name (first), job ID and department name

7. From the following table, write a SQL query 
to find the employee whose salary is 3000 and reporting person’s ID is 121.
Return all fields.  ==> needs ONE person(1 row) as a result

SELECT * 
FROM employees 
WHERE (salary,manager_id)=
(SELECT 3000,121);


8. From the following table, write a SQL query to find
 those employees whose ID matches any of the numbers 134, 159 and 183.
 Return all the fields  ==> 
 
SELECT * 
FROM employees 
WHERE employee_id IN (134,159,183);

9. From the following table, write a SQL query to
 find those employees whose salary is in the range of 1000, and 3000 
 (Begin and end values have included.).
 Return all the fields. 

SELECT * FROM employees 
WHERE salary BETWEEN 1000 and 3000;

10. From the following table and write a SQL query to 
find those employees whose salary falls within the 
range of the smallest salary and 2500.
 Return all the fields
 
SELECT * FROM employees 
WHERE salary BETWEEN  (SELECT MIN(salary) FROM employees) AND 2500;

11.From the following tables, write a SQL query to find those employees
 who do not work in the departments 
 where managers’ IDs are between 100 and 200 
Return all the fields of the employees.

SELECT * FROM employees WHERE department_id NOT IN 
(SELECT department_id 
FROM departments 
WHERE manager_id BETWEEN 100 AND 200); ==> close but no cigar  #FAILED

12. From the following table, write a SQL query to find those employees 
who get second-highest salary. 
Return all the fields of the employees

SELECT * FROM employees WHERE employee_id IN 
(SELECT employee_id FROM employees  WHERE salary = 
(SELECT MAX(salary) FROM employees WHERE salary <
(SELECT MAX(salary) FROM employees))); #FAILED

13. From the following tables, write a SQL query to find those employees who
work in the same department as ‘Clara’. 
Exclude all those records where first name is ‘Clara’.
Return first name, last name and hire date.

SELECT first_name, last_name, hire_date  FROM employees  
WHERE department_id =  (SELECT department_id FROM employees  
WHERE first_name = 'Clara')  
AND first_name <> 'Clara'; #FAILED

14. From the following tables, write a SQL query to find those employees
 who work in a department where the employee’s first name contains the letter 'T'.
 Return employee ID, first name and last name

Select employee_ID firstName, lastName
from employees
where Department_ID IN 
(
Select department_ID from employees where firstName Like '%T%'
)

15. From the following tables, write a SQL query to find those employees 
who earn more than the average salary 
and work in the same department as an employee 
whose first name contains the letter 'J'. 
Return employee ID, first name and salary. 

SELECT employee_id, first_name , salary  
FROM employees  
WHERE salary > 
(SELECT AVG (salary)  
FROM employees ) 
AND  department_id IN 
( SELECT department_id  
FROM employees  
WHERE first_name LIKE '%J%');
 
16. From the following table, write a SQL query to find those employees 
whose department is located at ‘Toronto’. 
Return first name, last name, employee ID, job ID

SELECT  first_name, last_name, employee_id, job_id  
FROM employees  
WHERE department_id = 
(SELECT department_id  
FROM departments 
WHERE location_id = 
(SELECT location_id 
FROM locations  
WHERE city ='Toronto'));

17. From the following table, write a SQL query to find those employees 
whose salary is lower than that of employees whose job title is ‘MK_MAN’.
Return employee ID, first name, last name, job ID

Select employee_ID firstName,LastName,Job_ID
from employees
where salary < ANY -- used to compare a range of values against 1 thing
(
select salary from employees
where Job_ID = 'MK_MAN'
)

18. From the following table, write a SQL query to find those employees 
whose salary is lower than that of employees whose job title is "MK_MAN".
Exclude employees of Job title ‘MK_MAN’. 
Return employee ID, first name, last name, job ID. 

Select employee_ID firstName,LastName,Job_ID
from employees
where salary < ANY -- used to compare a range of values against 1 thing
(
select salary from employees
where Job_ID = 'MK_MAN'
) AND Job_ID <> 'MK.MAN'

19. From the following table, write a SQL query to find those employees 
whose salary exceeds the salary of all those employees 
whose job title is "PU_MAN". Exclude job title ‘PU_MAN’. 
Return employee ID, first name, last name, job ID. 

Select employee_ID, firsName, lastName, Job_ID from employees
where salary > ALL 
(Select salary from employees where job_id = 'PU_MAN')
And Job_ID <> 'PU_MAN'

20. From the following table, write a SQL query to find those employees 
whose salaries are higher than the average for all departments. 
Return employee ID, first name, last name, job ID.

Select employee_ID,firstName,lastName,job_id from employees
where department_ID =
(
Select department_ID from employees 
where salary > ALL 
(Select avg(salary)from employees) 
)
-----------------------------------------------------------------
SELECT employee_id, first_name, last_name, job_id  
FROM employees  
WHERE salary > ALL  
( SELECT AVG(salary)  
FROM employees  
GROUP BY department_id 
);

21. From the following table, write a SQL query 
to check whether there are any employees with salaries exceeding 3700. 
Return first name, last name and department ID

Select firstName,lastName,Department_ID from employees
where Employee_Id = ANY 
(
Select Employee_Id from employees where salary > 3700
)

22. 