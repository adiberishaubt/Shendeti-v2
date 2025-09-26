import axios, { AxiosInstance, AxiosResponse } from 'axios';
import { 
  User, 
  Patient, 
  Doctor, 
  Appointment, 
  BloodRequest, 
  Donation, 
  Feedback,
  Country,
  City,
  Specialization,
  Education,
  Level,
  Service,
  Schedule,
  Slot,
  LoginRequest,
  RegisterPatientRequest,
  RegisterDoctorRequest,
  TokenResponse,
  DoctorSearchDto,
  FilterDoctorDto,
  PotentialDonor,
  PaginatedResponse,
  ApiResponse
} from '../types';

class ApiService {
  private api: AxiosInstance;

  constructor() {
    this.api = axios.create({
      baseURL: 'https://localhost:7161', // Backend API URL
      headers: {
        'Content-Type': 'application/json',
      },
    });

    // Add request interceptor to include auth token
    this.api.interceptors.request.use(
      (config) => {
        const token = localStorage.getItem('token');
        if (token) {
          config.headers.Authorization = `Bearer ${token}`;
        }
        return config;
      },
      (error) => {
        return Promise.reject(error);
      }
    );

    // Add response interceptor for error handling
    this.api.interceptors.response.use(
      (response) => response,
      (error) => {
        if (error.response?.status === 401) {
          localStorage.removeItem('token');
          window.location.href = '/login';
        }
        return Promise.reject(error);
      }
    );
  }

  // Authentication
  async login(credentials: LoginRequest): Promise<TokenResponse> {
    const response: AxiosResponse<TokenResponse> = await this.api.post('/User/login', credentials);
    return response.data;
  }

  async registerPatient(data: RegisterPatientRequest): Promise<ApiResponse<Patient>> {
    const response: AxiosResponse<ApiResponse<Patient>> = await this.api.post('/User/register-patient', data);
    return response.data;
  }

  async registerDoctor(data: RegisterDoctorRequest): Promise<ApiResponse<Doctor>> {
    const response: AxiosResponse<ApiResponse<Doctor>> = await this.api.post('/User/register-doctor', data);
    return response.data;
  }

  // Users
  async getCurrentUser(): Promise<User> {
    const response: AxiosResponse<User> = await this.api.get('/User/me');
    return response.data;
  }

  async updateUser(id: string, data: Partial<User>): Promise<User> {
    const response: AxiosResponse<User> = await this.api.put(`/User/${id}`, data);
    return response.data;
  }

  async deleteUser(id: string): Promise<void> {
    await this.api.delete(`/User/${id}`);
  }

  // Countries
  async getCountries(): Promise<Country[]> {
    const response: AxiosResponse<Country[]> = await this.api.get('/Country');
    return response.data;
  }

  async createCountry(data: { name: string; code: string }): Promise<Country> {
    const response: AxiosResponse<Country> = await this.api.post('/Country', data);
    return response.data;
  }

  async updateCountry(id: number, data: Partial<Country>): Promise<Country> {
    const response: AxiosResponse<Country> = await this.api.put(`/Country/${id}`, data);
    return response.data;
  }

  async deleteCountry(id: number): Promise<void> {
    await this.api.delete(`/Country/${id}`);
  }

  // Cities
  async getCities(): Promise<City[]> {
    const response: AxiosResponse<City[]> = await this.api.get('/City');
    return response.data;
  }

  async getCitiesByCountry(countryId: number): Promise<City[]> {
    const response: AxiosResponse<City[]> = await this.api.get(`/City/country/${countryId}`);
    return response.data;
  }

  async createCity(data: { name: string; countryId: number }): Promise<City> {
    const response: AxiosResponse<City> = await this.api.post('/City', data);
    return response.data;
  }

  async updateCity(id: number, data: Partial<City>): Promise<City> {
    const response: AxiosResponse<City> = await this.api.put(`/City/${id}`, data);
    return response.data;
  }

  async deleteCity(id: number): Promise<void> {
    await this.api.delete(`/City/${id}`);
  }

  // Specializations
  async getSpecializations(): Promise<Specialization[]> {
    const response: AxiosResponse<Specialization[]> = await this.api.get('/Specialization');
    return response.data;
  }

  async createSpecialization(data: { name: string; description: string }): Promise<Specialization> {
    const response: AxiosResponse<Specialization> = await this.api.post('/Specialization', data);
    return response.data;
  }

  async updateSpecialization(id: number, data: Partial<Specialization>): Promise<Specialization> {
    const response: AxiosResponse<Specialization> = await this.api.put(`/Specialization/${id}`, data);
    return response.data;
  }

  async deleteSpecialization(id: number): Promise<void> {
    await this.api.delete(`/Specialization/${id}`);
  }

  // Education
  async getEducations(): Promise<Education[]> {
    const response: AxiosResponse<Education[]> = await this.api.get('/Education');
    return response.data;
  }

  async createEducation(data: { name: string; description: string }): Promise<Education> {
    const response: AxiosResponse<Education> = await this.api.post('/Education', data);
    return response.data;
  }

  async updateEducation(id: number, data: Partial<Education>): Promise<Education> {
    const response: AxiosResponse<Education> = await this.api.put(`/Education/${id}`, data);
    return response.data;
  }

  async deleteEducation(id: number): Promise<void> {
    await this.api.delete(`/Education/${id}`);
  }

  // Levels
  async getLevels(): Promise<Level[]> {
    const response: AxiosResponse<Level[]> = await this.api.get('/Level');
    return response.data;
  }

  async createLevel(data: { name: string; description: string }): Promise<Level> {
    const response: AxiosResponse<Level> = await this.api.post('/Level', data);
    return response.data;
  }

  async updateLevel(id: number, data: Partial<Level>): Promise<Level> {
    const response: AxiosResponse<Level> = await this.api.put(`/Level/${id}`, data);
    return response.data;
  }

  async deleteLevel(id: number): Promise<void> {
    await this.api.delete(`/Level/${id}`);
  }

  // Services
  async getServices(): Promise<Service[]> {
    const response: AxiosResponse<Service[]> = await this.api.get('/Service');
    return response.data;
  }

  async createService(data: { name: string; description: string; price: number }): Promise<Service> {
    const response: AxiosResponse<Service> = await this.api.post('/Service', data);
    return response.data;
  }

  async updateService(id: number, data: Partial<Service>): Promise<Service> {
    const response: AxiosResponse<Service> = await this.api.put(`/Service/${id}`, data);
    return response.data;
  }

  async deleteService(id: number): Promise<void> {
    await this.api.delete(`/Service/${id}`);
  }

  // Schedules
  async getSchedules(): Promise<Schedule[]> {
    const response: AxiosResponse<Schedule[]> = await this.api.get('/Schedule');
    return response.data;
  }

  async getSchedulesByDoctor(doctorId: string): Promise<Schedule[]> {
    const response: AxiosResponse<Schedule[]> = await this.api.get(`/Schedule/doctor/${doctorId}`);
    return response.data;
  }

  async createSchedule(data: {
    doctorId: string;
    day: string;
    startTime: string;
    endTime: string;
  }): Promise<Schedule> {
    const response: AxiosResponse<Schedule> = await this.api.post('/Schedule', data);
    return response.data;
  }

  async updateSchedule(id: number, data: Partial<Schedule>): Promise<Schedule> {
    const response: AxiosResponse<Schedule> = await this.api.put(`/Schedule/${id}`, data);
    return response.data;
  }

  async deleteSchedule(id: number): Promise<void> {
    await this.api.delete(`/Schedule/${id}`);
  }

  // Slots
  async getSlots(): Promise<Slot[]> {
    const response: AxiosResponse<Slot[]> = await this.api.get('/Slots');
    return response.data;
  }

  async getSlotsBySchedule(scheduleId: number): Promise<Slot[]> {
    const response: AxiosResponse<Slot[]> = await this.api.get(`/Slots/schedule/${scheduleId}`);
    return response.data;
  }

  async createSlot(data: {
    scheduleId: number;
    startTime: string;
    endTime: string;
  }): Promise<Slot> {
    const response: AxiosResponse<Slot> = await this.api.post('/Slots', data);
    return response.data;
  }

  async updateSlot(id: number, data: Partial<Slot>): Promise<Slot> {
    const response: AxiosResponse<Slot> = await this.api.put(`/Slots/${id}`, data);
    return response.data;
  }

  async deleteSlot(id: number): Promise<void> {
    await this.api.delete(`/Slots/${id}`);
  }

  // Appointments
  async getAppointments(): Promise<Appointment[]> {
    const response: AxiosResponse<Appointment[]> = await this.api.get('/Appointment');
    return response.data;
  }

  async getAppointmentsByPatient(patientId: string): Promise<Appointment[]> {
    const response: AxiosResponse<Appointment[]> = await this.api.get(`/Appointment/patient/${patientId}`);
    return response.data;
  }

  async getAppointmentsByDoctor(doctorId: string): Promise<Appointment[]> {
    const response: AxiosResponse<Appointment[]> = await this.api.get(`/Appointment/doctor/${doctorId}`);
    return response.data;
  }

  async createAppointment(data: {
    patientId: string;
    doctorId: string;
    slotId: number;
    appointmentDate: string;
    notes?: string;
    meetingType: string;
  }): Promise<Appointment> {
    const response: AxiosResponse<Appointment> = await this.api.post('/Appointment', data);
    return response.data;
  }

  async updateAppointment(id: number, data: Partial<Appointment>): Promise<Appointment> {
    const response: AxiosResponse<Appointment> = await this.api.put(`/Appointment/${id}`, data);
    return response.data;
  }

  async cancelAppointment(id: number): Promise<void> {
    await this.api.post(`/Appointment/${id}/cancel`);
  }

  async completeAppointment(id: number): Promise<void> {
    await this.api.post(`/Appointment/${id}/complete`);
  }

  // Blood Requests
  async getBloodRequests(): Promise<BloodRequest[]> {
    const response: AxiosResponse<BloodRequest[]> = await this.api.get('/BloodRequest');
    return response.data;
  }

  async getBloodRequestsByPatient(patientId: string): Promise<BloodRequest[]> {
    const response: AxiosResponse<BloodRequest[]> = await this.api.get(`/BloodRequest/patient/${patientId}`);
    return response.data;
  }

  async createBloodRequest(data: {
    bloodType: string;
    urgency: string;
    quantity: number;
    reason: string;
    hospitalName: string;
    hospitalAddress: string;
    contactPerson: string;
    contactPhone: string;
  }): Promise<BloodRequest> {
    const response: AxiosResponse<BloodRequest> = await this.api.post('/BloodRequest', data);
    return response.data;
  }

  async acceptBloodRequest(id: number): Promise<void> {
    await this.api.post(`/BloodRequest/${id}/accept`);
  }

  async confirmBloodRequestCompletion(requestId: number, donorId: string): Promise<void> {
    await this.api.post(`/BloodRequest/${requestId}/donor/${donorId}/confirm`);
  }

  async deleteBloodRequest(id: number): Promise<void> {
    await this.api.delete(`/BloodRequest/${id}`);
  }

  // Donations
  async getDonations(): Promise<Donation[]> {
    const response: AxiosResponse<Donation[]> = await this.api.get('/Donation');
    return response.data;
  }

  async getDonationsByDonor(donorId: string): Promise<Donation[]> {
    const response: AxiosResponse<Donation[]> = await this.api.get(`/Donation/donor/${donorId}`);
    return response.data;
  }

  async createDonation(data: {
    donorId: string;
    bloodRequestId?: number;
    bloodType: string;
    quantity: number;
    donationDate: string;
    notes?: string;
  }): Promise<Donation> {
    const response: AxiosResponse<Donation> = await this.api.post('/Donation', data);
    return response.data;
  }

  async updateDonation(id: number, data: Partial<Donation>): Promise<Donation> {
    const response: AxiosResponse<Donation> = await this.api.put(`/Donation/${id}`, data);
    return response.data;
  }

  async deleteDonation(id: number): Promise<void> {
    await this.api.delete(`/Donation/${id}`);
  }

  // Feedback
  async getFeedback(): Promise<Feedback[]> {
    const response: AxiosResponse<Feedback[]> = await this.api.get('/Feedback');
    return response.data;
  }

  async createFeedback(data: {
    appointmentId?: number;
    rating: number;
    comment: string;
  }): Promise<Feedback> {
    const response: AxiosResponse<Feedback> = await this.api.post('/Feedback', data);
    return response.data;
  }

  async updateFeedback(id: number, data: Partial<Feedback>): Promise<Feedback> {
    const response: AxiosResponse<Feedback> = await this.api.put(`/Feedback/${id}`, data);
    return response.data;
  }

  async deleteFeedback(id: number): Promise<void> {
    await this.api.delete(`/Feedback/${id}`);
  }

  // Doctor Search
  async searchDoctors(filters: FilterDoctorDto): Promise<PaginatedResponse<Doctor>> {
    const response: AxiosResponse<PaginatedResponse<Doctor>> = await this.api.post('/User/search-doctors', filters);
    return response.data;
  }

  async getPotentialDonors(bloodType: string, cityId?: number): Promise<PotentialDonor[]> {
    const params = new URLSearchParams({ bloodType });
    if (cityId) params.append('cityId', cityId.toString());
    
    const response: AxiosResponse<PotentialDonor[]> = await this.api.get(`/User/potential-donors?${params}`);
    return response.data;
  }
}

export default new ApiService();
