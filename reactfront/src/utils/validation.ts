import * as yup from 'yup';

// Validation schemas for different forms
export const loginSchema = yup.object({
  email: yup
    .string()
    .email('Invalid email address')
    .required('Email is required'),
  password: yup
    .string()
    .min(6, 'Password must be at least 6 characters')
    .required('Password is required'),
});

export const registerPatientSchema = yup.object({
  email: yup
    .string()
    .email('Invalid email address')
    .required('Email is required'),
  password: yup
    .string()
    .min(6, 'Password must be at least 6 characters')
    .required('Password is required'),
  firstName: yup
    .string()
    .required('First name is required'),
  lastName: yup
    .string()
    .required('Last name is required'),
  phoneNumber: yup
    .string()
    .required('Phone number is required'),
  dateOfBirth: yup
    .date()
    .required('Date of birth is required'),
  bloodType: yup
    .string()
    .oneOf(['A+', 'A-', 'B+', 'B-', 'AB+', 'AB-', 'O+', 'O-'], 'Invalid blood type')
    .required('Blood type is required'),
  address: yup
    .string()
    .required('Address is required'),
  cityId: yup
    .number()
    .required('City is required'),
  countryId: yup
    .number()
    .required('Country is required'),
});

export const registerDoctorSchema = yup.object({
  email: yup
    .string()
    .email('Invalid email address')
    .required('Email is required'),
  password: yup
    .string()
    .min(6, 'Password must be at least 6 characters')
    .required('Password is required'),
  firstName: yup
    .string()
    .required('First name is required'),
  lastName: yup
    .string()
    .required('Last name is required'),
  phoneNumber: yup
    .string()
    .required('Phone number is required'),
  specializationId: yup
    .number()
    .required('Specialization is required'),
  educationId: yup
    .number()
    .required('Education is required'),
  levelId: yup
    .number()
    .required('Level is required'),
  licenseNumber: yup
    .string()
    .required('License number is required'),
  experience: yup
    .number()
    .min(0, 'Experience must be 0 or more')
    .required('Experience is required'),
  bio: yup
    .string()
    .required('Bio is required'),
  consultationFee: yup
    .number()
    .min(0, 'Consultation fee must be 0 or more')
    .required('Consultation fee is required'),
});

export const appointmentSchema = yup.object({
  doctorId: yup
    .string()
    .required('Doctor is required'),
  slotId: yup
    .number()
    .required('Time slot is required'),
  appointmentDate: yup
    .date()
    .min(new Date(), 'Appointment date must be in the future')
    .required('Appointment date is required'),
  notes: yup
    .string(),
  meetingType: yup
    .string()
    .oneOf(['InPerson', 'Online'], 'Invalid meeting type')
    .required('Meeting type is required'),
});

export const bloodRequestSchema = yup.object({
  bloodType: yup
    .string()
    .oneOf(['A+', 'A-', 'B+', 'B-', 'AB+', 'AB-', 'O+', 'O-'], 'Invalid blood type')
    .required('Blood type is required'),
  urgency: yup
    .string()
    .oneOf(['Low', 'Medium', 'High', 'Critical'], 'Invalid urgency level')
    .required('Urgency level is required'),
  quantity: yup
    .number()
    .min(1, 'Quantity must be at least 1')
    .required('Quantity is required'),
  reason: yup
    .string()
    .required('Reason is required'),
  hospitalName: yup
    .string()
    .required('Hospital name is required'),
  hospitalAddress: yup
    .string()
    .required('Hospital address is required'),
  contactPerson: yup
    .string()
    .required('Contact person is required'),
  contactPhone: yup
    .string()
    .required('Contact phone is required'),
});

export const donationSchema = yup.object({
  donorId: yup
    .string()
    .required('Donor is required'),
  bloodRequestId: yup
    .number(),
  bloodType: yup
    .string()
    .oneOf(['A+', 'A-', 'B+', 'B-', 'AB+', 'AB-', 'O+', 'O-'], 'Invalid blood type')
    .required('Blood type is required'),
  quantity: yup
    .number()
    .min(1, 'Quantity must be at least 1')
    .required('Quantity is required'),
  donationDate: yup
    .date()
    .max(new Date(), 'Donation date cannot be in the future')
    .required('Donation date is required'),
  notes: yup
    .string(),
});

export const feedbackSchema = yup.object({
  appointmentId: yup
    .number(),
  rating: yup
    .number()
    .min(1, 'Rating must be at least 1')
    .max(5, 'Rating must be at most 5')
    .required('Rating is required'),
  comment: yup
    .string()
    .required('Comment is required'),
});

// Utility functions for validation
export const validateEmail = (email: string): boolean => {
  const emailRegex = /^[A-Z0-9._%+-]+@[A-Z0-9.-]+\.[A-Z]{2,}$/i;
  return emailRegex.test(email);
};

export const validatePhoneNumber = (phone: string): boolean => {
  const phoneRegex = /^[\+]?[1-9][\d]{0,15}$/;
  return phoneRegex.test(phone.replace(/\s/g, ''));
};

export const validateBloodType = (bloodType: string): boolean => {
  const validBloodTypes = ['A+', 'A-', 'B+', 'B-', 'AB+', 'AB-', 'O+', 'O-'];
  return validBloodTypes.includes(bloodType);
};

export const validatePassword = (password: string): { isValid: boolean; errors: string[] } => {
  const errors: string[] = [];
  
  if (password.length < 6) {
    errors.push('Password must be at least 6 characters long');
  }
  
  if (!/(?=.*[a-z])/.test(password)) {
    errors.push('Password must contain at least one lowercase letter');
  }
  
  if (!/(?=.*[A-Z])/.test(password)) {
    errors.push('Password must contain at least one uppercase letter');
  }
  
  if (!/(?=.*\d)/.test(password)) {
    errors.push('Password must contain at least one number');
  }
  
  return {
    isValid: errors.length === 0,
    errors,
  };
};
