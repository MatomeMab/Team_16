import { Time } from "@angular/common";

export class Schedule {
    sheduleId!: string;
    sheduleName!: string;
    date!: Date;
    startTime!: Time;
    endTime!: Time;

}
export class Employees {
    employeeId!: string;
    employeeName!: string;
    employeeSurname!: string;
    

}
export class ScheduleResponse{
    date!: Date;
    startTime!: Time;
    endTime!: Time;

}
export class ScheduleTypeRequest{
    date!: Date;
    startTime!: Time;
    endTime!: Time;

}
