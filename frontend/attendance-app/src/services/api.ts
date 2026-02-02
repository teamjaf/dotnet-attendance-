import axios from 'axios';
import type { Employee, CreateEmployeeRequest, AttendanceRecord, DashboardStats } from '../types';

const API_URL = import.meta.env.VITE_API_URL || 'http://localhost:5000/api';

const api = axios.create({
  baseURL: API_URL,
  headers: {
    'Content-Type': 'application/json',
  },
});

// Dashboard
export const getDashboardStats = async (): Promise<DashboardStats> => {
  const { data } = await api.get('/dashboard/stats');
  return data;
};

// Employees
export const getEmployees = async (): Promise<Employee[]> => {
  const { data } = await api.get('/employees');
  return data;
};

export const getEmployee = async (id: number): Promise<Employee> => {
  const { data } = await api.get(`/employees/${id}`);
  return data;
};

export const createEmployee = async (employee: CreateEmployeeRequest): Promise<Employee> => {
  const { data } = await api.post('/employees', employee);
  return data;
};

export const updateEmployee = async (id: number, employee: Partial<Employee>): Promise<Employee> => {
  const { data } = await api.put(`/employees/${id}`, employee);
  return data;
};

// Attendance
export const getAttendance = async (fromDate?: string, toDate?: string): Promise<AttendanceRecord[]> => {
  const params = new URLSearchParams();
  if (fromDate) params.append('fromDate', fromDate);
  if (toDate) params.append('toDate', toDate);
  const { data } = await api.get(`/attendance?${params.toString()}`);
  return data;
};
