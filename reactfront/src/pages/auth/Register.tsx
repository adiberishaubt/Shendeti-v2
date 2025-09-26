import React, { useState } from 'react';
import { Link, useNavigate } from 'react-router-dom';
import { useForm } from 'react-hook-form';
import api from '../../services/api';
import { RegisterPatientRequest, RegisterDoctorRequest } from '../../types';

const Register: React.FC = () => {
  const navigate = useNavigate();
  const [userType, setUserType] = useState<'Patient' | 'Doctor'>('Patient');
  const [isLoading, setIsLoading] = useState(false);
  const [error, setError] = useState<string | null>(null);

  const { register, handleSubmit, formState: { errors }, watch } = useForm<RegisterPatientRequest | RegisterDoctorRequest>();

  const onSubmit = async (data: any) => {
    setIsLoading(true);
    setError(null);

    try {
      if (userType === 'Patient') {
        await api.registerPatient(data as RegisterPatientRequest);
      } else {
        await api.registerDoctor(data as RegisterDoctorRequest);
      }
      navigate('/login');
    } catch (err: any) {
      setError(err.response?.data?.message || 'Registration failed');
    } finally {
      setIsLoading(false);
    }
  };

  return (
    <div>
      <div className="text-center">
        <h2 className="text-2xl font-bold text-gray-900">Create your account</h2>
        <p className="mt-2 text-sm text-gray-600">
          Or{' '}
          <Link to="/login" className="font-medium text-primary-600 hover:text-primary-500">
            sign in to your existing account
          </Link>
        </p>
      </div>

      <div className="mt-6">
        <div className="flex rounded-md shadow-sm">
          <button
            type="button"
            onClick={() => setUserType('Patient')}
            className={`flex-1 py-2 px-4 text-sm font-medium rounded-l-md border ${
              userType === 'Patient'
                ? 'bg-primary-600 text-white border-primary-600'
                : 'bg-white text-gray-700 border-gray-300 hover:bg-gray-50'
            }`}
          >
            Patient
          </button>
          <button
            type="button"
            onClick={() => setUserType('Doctor')}
            className={`flex-1 py-2 px-4 text-sm font-medium rounded-r-md border ${
              userType === 'Doctor'
                ? 'bg-primary-600 text-white border-primary-600'
                : 'bg-white text-gray-700 border-gray-300 hover:bg-gray-50'
            }`}
          >
            Doctor
          </button>
        </div>
      </div>

      <form className="mt-8 space-y-6" onSubmit={handleSubmit(onSubmit)}>
        {error && (
          <div className="bg-red-50 border border-red-200 text-red-600 px-4 py-3 rounded-md">
            {error}
          </div>
        )}

        <div className="space-y-4">
          {/* Common fields */}
          <div className="grid grid-cols-2 gap-4">
            <div>
              <label htmlFor="firstName" className="block text-sm font-medium text-gray-700">
                First Name
              </label>
              <input
                {...register('firstName', { required: 'First name is required' })}
                type="text"
                className="input-field mt-1"
                placeholder="First name"
              />
              {errors.firstName && (
                <p className="mt-1 text-sm text-red-600">{errors.firstName.message}</p>
              )}
            </div>
            <div>
              <label htmlFor="lastName" className="block text-sm font-medium text-gray-700">
                Last Name
              </label>
              <input
                {...register('lastName', { required: 'Last name is required' })}
                type="text"
                className="input-field mt-1"
                placeholder="Last name"
              />
              {errors.lastName && (
                <p className="mt-1 text-sm text-red-600">{errors.lastName.message}</p>
              )}
            </div>
          </div>

          <div>
            <label htmlFor="email" className="block text-sm font-medium text-gray-700">
              Email address
            </label>
            <input
              {...register('email', { 
                required: 'Email is required',
                pattern: {
                  value: /^[A-Z0-9._%+-]+@[A-Z0-9.-]+\.[A-Z]{2,}$/i,
                  message: 'Invalid email address'
                }
              })}
              type="email"
              className="input-field mt-1"
              placeholder="Enter your email"
            />
            {errors.email && (
              <p className="mt-1 text-sm text-red-600">{errors.email.message}</p>
            )}
          </div>

          <div>
            <label htmlFor="password" className="block text-sm font-medium text-gray-700">
              Password
            </label>
            <input
              {...register('password', { 
                required: 'Password is required',
                minLength: {
                  value: 6,
                  message: 'Password must be at least 6 characters'
                }
              })}
              type="password"
              className="input-field mt-1"
              placeholder="Enter your password"
            />
            {errors.password && (
              <p className="mt-1 text-sm text-red-600">{errors.password.message}</p>
            )}
          </div>

          <div>
            <label htmlFor="phoneNumber" className="block text-sm font-medium text-gray-700">
              Phone Number
            </label>
            <input
              {...register('phoneNumber', { required: 'Phone number is required' })}
              type="tel"
              className="input-field mt-1"
              placeholder="Enter your phone number"
            />
            {errors.phoneNumber && (
              <p className="mt-1 text-sm text-red-600">{errors.phoneNumber.message}</p>
            )}
          </div>

          {/* Patient specific fields */}
          {userType === 'Patient' && (
            <>
              <div>
                <label htmlFor="dateOfBirth" className="block text-sm font-medium text-gray-700">
                  Date of Birth
                </label>
                <input
                  {...register('dateOfBirth', { required: 'Date of birth is required' })}
                  type="date"
                  className="input-field mt-1"
                />
                {errors.dateOfBirth && (
                  <p className="mt-1 text-sm text-red-600">{errors.dateOfBirth.message}</p>
                )}
              </div>

              <div>
                <label htmlFor="bloodType" className="block text-sm font-medium text-gray-700">
                  Blood Type
                </label>
                <select
                  {...register('bloodType', { required: 'Blood type is required' })}
                  className="input-field mt-1"
                >
                  <option value="">Select blood type</option>
                  <option value="A+">A+</option>
                  <option value="A-">A-</option>
                  <option value="B+">B+</option>
                  <option value="B-">B-</option>
                  <option value="AB+">AB+</option>
                  <option value="AB-">AB-</option>
                  <option value="O+">O+</option>
                  <option value="O-">O-</option>
                </select>
                {errors.bloodType && (
                  <p className="mt-1 text-sm text-red-600">{errors.bloodType.message}</p>
                )}
              </div>

              <div>
                <label htmlFor="address" className="block text-sm font-medium text-gray-700">
                  Address
                </label>
                <textarea
                  {...register('address', { required: 'Address is required' })}
                  rows={3}
                  className="input-field mt-1"
                  placeholder="Enter your address"
                />
                {errors.address && (
                  <p className="mt-1 text-sm text-red-600">{errors.address.message}</p>
                )}
              </div>
            </>
          )}

          {/* Doctor specific fields */}
          {userType === 'Doctor' && (
            <>
              <div>
                <label htmlFor="licenseNumber" className="block text-sm font-medium text-gray-700">
                  License Number
                </label>
                <input
                  {...register('licenseNumber', { required: 'License number is required' })}
                  type="text"
                  className="input-field mt-1"
                  placeholder="Enter your license number"
                />
                {errors.licenseNumber && (
                  <p className="mt-1 text-sm text-red-600">{errors.licenseNumber.message}</p>
                )}
              </div>

              <div>
                <label htmlFor="experience" className="block text-sm font-medium text-gray-700">
                  Years of Experience
                </label>
                <input
                  {...register('experience', { 
                    required: 'Experience is required',
                    min: { value: 0, message: 'Experience must be 0 or more' }
                  })}
                  type="number"
                  min="0"
                  className="input-field mt-1"
                  placeholder="Enter years of experience"
                />
                {errors.experience && (
                  <p className="mt-1 text-sm text-red-600">{errors.experience.message}</p>
                )}
              </div>

              <div>
                <label htmlFor="consultationFee" className="block text-sm font-medium text-gray-700">
                  Consultation Fee
                </label>
                <input
                  {...register('consultationFee', { 
                    required: 'Consultation fee is required',
                    min: { value: 0, message: 'Fee must be 0 or more' }
                  })}
                  type="number"
                  min="0"
                  step="0.01"
                  className="input-field mt-1"
                  placeholder="Enter consultation fee"
                />
                {errors.consultationFee && (
                  <p className="mt-1 text-sm text-red-600">{errors.consultationFee.message}</p>
                )}
              </div>

              <div>
                <label htmlFor="bio" className="block text-sm font-medium text-gray-700">
                  Bio
                </label>
                <textarea
                  {...register('bio', { required: 'Bio is required' })}
                  rows={4}
                  className="input-field mt-1"
                  placeholder="Tell us about yourself"
                />
                {errors.bio && (
                  <p className="mt-1 text-sm text-red-600">{errors.bio.message}</p>
                )}
              </div>
            </>
          )}
        </div>

        <div>
          <button
            type="submit"
            disabled={isLoading}
            className="btn-primary w-full"
          >
            {isLoading ? 'Creating account...' : 'Create account'}
          </button>
        </div>
      </form>
    </div>
  );
};

export default Register;
