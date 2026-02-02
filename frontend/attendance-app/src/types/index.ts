export interface Employee {
  id: number;
  code: string;
  name: string;
  department: string;
  isActive: boolean;
}

export interface CreateEmployeeRequest {
  code: string;
  name: string;
  department: string;
}

export interface AttendanceRecord {
  id: number;
  employeeId: number;
  employeeCode: string;
  employeeName: string;
  punchTime: string;
  punchType: string;
  deviceId: string;
}

export interface DashboardStats {
  totalEmployees: number;
  presentToday: number;
  absentToday: number;
  totalPunchesToday: number;
}
