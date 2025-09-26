// User Types
export interface User {
  id: string;
  email: string;
  firstName: string;
  lastName: string;
  phoneNumber: string;
  role: 'Patient' | 'Doctor' | 'Admin';
  createdAt: string;
  updatedAt: string;
}

export interface Patient extends User {
  role: 'Patient';
  dateOfBirth: string;
  bloodType: BloodType;
  address: string;
  cityId: number;
  countryId: number;
}

export interface Doctor extends User {
  role: 'Doctor';
  specializationId: number;
  educationId: number;
  levelId: number;
  licenseNumber: string;
  experience: number;
  bio: string;
  consultationFee: number;
}

export interface Admin extends User {
  role: 'Admin';
}

// Blood Types
export type BloodType = 'A+' | 'A-' | 'B+' | 'B-' | 'AB+' | 'AB-' | 'O+' | 'O-';

// Location Types
export interface Country {
  id: number;
  name: string;
  code: string;
}

export interface City {
  id: number;
  name: string;
  countryId: number;
  country?: Country;
}

// Medical Types
export interface Specialization {
  id: number;
  name: string;
  description: string;
}

export interface Education {
  id: number;
  name: string;
  description: string;
}

export interface Level {
  id: number;
  name: string;
  description: string;
}

export interface Service {
  id: number;
  name: string;
  description: string;
  price: number;
}

// Schedule Types
export interface Schedule {
  id: number;
  doctorId: string;
  day: Day;
  startTime: string;
  endTime: string;
  isActive: boolean;
  doctor?: Doctor;
}

export type Day = 'Monday' | 'Tuesday' | 'Wednesday' | 'Thursday' | 'Friday' | 'Saturday' | 'Sunday';

export interface Slot {
  id: number;
  scheduleId: number;
  startTime: string;
  endTime: string;
  isAvailable: boolean;
  schedule?: Schedule;
}

// Appointment Types
export interface Appointment {
  id: number;
  patientId: string;
  doctorId: string;
  slotId: number;
  appointmentDate: string;
  status: AppointmentStatus;
  notes?: string;
  meetingType: MeetingType;
  meetingLink?: string;
  patient?: Patient;
  doctor?: Doctor;
  slot?: Slot;
}

export type AppointmentStatus = 'Scheduled' | 'Completed' | 'Cancelled' | 'NoShow';
export type MeetingType = 'InPerson' | 'Online';

// Blood Donation Types
export interface BloodRequest {
  id: number;
  patientId: string;
  bloodType: BloodType;
  urgency: Urgency;
  quantity: number;
  reason: string;
  hospitalName: string;
  hospitalAddress: string;
  contactPerson: string;
  contactPhone: string;
  status: BloodRequestStatus;
  createdAt: string;
  patient?: Patient;
}

export type Urgency = 'Low' | 'Medium' | 'High' | 'Critical';
export type BloodRequestStatus = 'Pending' | 'Accepted' | 'Completed' | 'Cancelled';

export interface Donation {
  id: number;
  donorId: string;
  bloodRequestId?: number;
  bloodType: BloodType;
  quantity: number;
  donationDate: string;
  status: DonationStatus;
  notes?: string;
  donor?: User;
  bloodRequest?: BloodRequest;
}

export type DonationStatus = 'Scheduled' | 'Completed' | 'Cancelled';

// Feedback Types
export interface Feedback {
  id: number;
  userId: string;
  appointmentId?: number;
  rating: number;
  comment: string;
  createdAt: string;
  user?: User;
  appointment?: Appointment;
}

// API Response Types
export interface ApiResponse<T> {
  data: T;
  message: string;
  success: boolean;
}

export interface PaginatedResponse<T> {
  data: T[];
  totalCount: number;
  pageNumber: number;
  pageSize: number;
  totalPages: number;
}

// Authentication Types
export interface LoginRequest {
  email: string;
  password: string;
}

export interface RegisterRequest {
  email: string;
  password: string;
  firstName: string;
  lastName: string;
  phoneNumber: string;
  role: 'Patient' | 'Doctor';
}

export interface TokenResponse {
  token: string;
  refreshToken: string;
  expiresAt: string;
}

// Form Types
export interface RegisterPatientRequest extends RegisterRequest {
  role: 'Patient';
  dateOfBirth: string;
  bloodType: BloodType;
  address: string;
  cityId: number;
  countryId: number;
}

export interface RegisterDoctorRequest extends RegisterRequest {
  role: 'Doctor';
  specializationId: number;
  educationId: number;
  levelId: number;
  licenseNumber: string;
  experience: number;
  bio: string;
  consultationFee: number;
}

// Search and Filter Types
export interface DoctorSearchDto {
  specializationId?: number;
  cityId?: number;
  countryId?: number;
  minExperience?: number;
  maxConsultationFee?: number;
  searchTerm?: string;
}

export interface FilterDoctorDto {
  specializationId?: number;
  cityId?: number;
  countryId?: number;
  minExperience?: number;
  maxConsultationFee?: number;
  searchTerm?: string;
  pageNumber?: number;
  pageSize?: number;
}

export interface PotentialDonor {
  id: string;
  firstName: string;
  lastName: string;
  email: string;
  phoneNumber: string;
  bloodType: BloodType;
  cityId: number;
  city?: City;
}
