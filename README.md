```
   My new REST Api named HiManager.

   DB Information: MySQL - Relational Database

   This REST API is build for my mobile application project named HiManager which is developped in React Native.
```

1) AUTHENTICATION BY EMAIL AND PASSWORD.

```
  SUCCESSFUL AUTHENTICATION
```

<img width="1087" alt="Login_Successful" src="https://github.com/user-attachments/assets/818d69ee-8245-4f14-a2c9-40436cd3044e">


```
  AUTHENTICATION FAILED
```

<img width="1087" alt="Login_Failed" src="https://github.com/user-attachments/assets/687e2967-8553-4994-ae72-31cdf8e5bfe6">


2) GET THE CURRENT USER.

```
  AUTHENTICATED USER
```

<img width="1087" alt="Get_Current_User" src="https://github.com/user-attachments/assets/f52cafb9-9ccf-4306-9005-4c094e240c25">

3) GET ALL USERS IN THE SYSTEM.

<img width="1087" alt="Get_All_Successful" src="https://github.com/user-attachments/assets/fc56acd7-249e-4ff0-bf56-2f391a810e18">

4) GET USER BY ID.

```
  SUCCESSFUL : If the given id exists.
```

<img width="1087" alt="Get_User_Successful" src="https://github.com/user-attachments/assets/36f04f72-a736-4663-8fa2-75262bd8dcb0">


```
  NOT FOUND : If the given id is does not exist.
```

<img width="1087" alt="Get_User_NotFound" src="https://github.com/user-attachments/assets/6bbb1f19-b821-4c18-b0eb-3bb76cd5a5b6">

5) CREATE A NEW USER.

```
  CREATE SUCCESSFUL
```

<img width="1087" alt="Create_Successful" src="https://github.com/user-attachments/assets/de8e3b5a-e0c0-46ce-b1f0-5dcdecce647a">


```
  CREATE FAILED
```

<img width="1085" alt="Create_Failed" src="https://github.com/user-attachments/assets/9a798e24-0082-45b7-96d3-c966697b691a">


6) UPDATE USER INFORMATION.

```
  There is no user update failure. Because if the given fields is not found on the user model, nothing will be updated. If it does, then it will be updated. 
```

<img width="1087" alt="Update_Successful" src="https://github.com/user-attachments/assets/b5e95514-89a5-4c6e-adfe-1a95e758c92d">

7) DELETE USER.

```
  DELETE SUCCESSFUL : If the current user has authority on target user.
```

<img width="1087" alt="Delete_Successful" src="https://github.com/user-attachments/assets/45325263-9b2c-4bb1-bc8c-20d350e5cef3">

```
  DELETE WARNING: Hirearchy problem.
```

<img width="1087" alt="Delete_Role_Hierarchy" src="https://github.com/user-attachments/assets/8ab4e1ce-1777-451a-9f5f-650710599f1c">

```
  DELETE FAILED : Unless the current user has authority on target user.
```

<img width="1087" alt="Delete_Failed" src="https://github.com/user-attachments/assets/585c2e5f-08b3-4260-8bd2-d9c6dbaef516">


8) ROLE APPOINTMENTS.

```
  ROLE APPOINTMENT SUCCESSFUL : If the current user has authority on target user.
```

<img width="1087" alt="Role_Appointment_Successful" src="https://github.com/user-attachments/assets/4a92ab34-a09f-4eec-a387-85827fdb373d">


```
  ROLE APPOINTMENT FAILED : Unless the current user has authority on target user.
```

<img width="1087" alt="Role_Appointment_Failed" src="https://github.com/user-attachments/assets/79f9879a-7739-4c69-8f6a-2884f9a09819">


9) GET USERS BY ROLES.

```
   Example user list, by given role name in the url.
```

<img width="1087" alt="Get_Users_With_Roles" src="https://github.com/user-attachments/assets/928355d9-87c6-41f1-992c-ae7066d7f6ff">
