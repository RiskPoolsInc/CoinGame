namespace App.Core.Enums; 

public enum AuditEventTypes {
    TaskCreated = 1,
    TaskChanged = 2,
    TaskAssigned = 3,
    TaskAssignedUserRemoved = 4,
    UserCreated =5,
    
    TaskRequestCreated = 10,
    TaskRequestChanged = 11,
    TaskActivated = 12,
    UserBlocked = 13,
    
    UserChanged = 22
    
       
    // 3,Task Assigned,TaskAssigned
    // 4,Task Assigned User Removed,TaskAssignedUserRemoved
    // 5,User Created,UserCreated
    //
    // 12,Task Activated,TaskActivated
    //UserBlocked = 13,
    //UserChanged = 22
    

}