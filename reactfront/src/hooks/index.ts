import { useEffect } from 'react';
import { useDispatch, useSelector } from 'react-redux';
import { RootState } from '../store';
import { fetchCountries } from '../store/slices/countrySlice';
import { fetchCities } from '../store/slices/citySlice';
import { fetchSpecializations } from '../store/slices/specializationSlice';
import { fetchEducations } from '../store/slices/educationSlice';
import { fetchLevels } from '../store/slices/levelSlice';
import { fetchServices } from '../store/slices/serviceSlice';

// Hook to load master data
export const useMasterData = () => {
  const dispatch = useDispatch();
  const countries = useSelector((state: RootState) => state.countries.countries);
  const cities = useSelector((state: RootState) => state.cities.cities);
  const specializations = useSelector((state: RootState) => state.specializations.specializations);
  const educations = useSelector((state: RootState) => state.educations.educations);
  const levels = useSelector((state: RootState) => state.levels.levels);
  const services = useSelector((state: RootState) => state.services.services);

  useEffect(() => {
    dispatch(fetchCountries());
    dispatch(fetchCities());
    dispatch(fetchSpecializations());
    dispatch(fetchEducations());
    dispatch(fetchLevels());
    dispatch(fetchServices());
  }, [dispatch]);

  return {
    countries,
    cities,
    specializations,
    educations,
    levels,
    services,
  };
};

// Hook for authentication state
export const useAuth = () => {
  const { user, isAuthenticated, isLoading, error } = useSelector((state: RootState) => state.auth);
  
  return {
    user,
    isAuthenticated,
    isLoading,
    error,
    isAdmin: user?.role === 'Admin',
    isDoctor: user?.role === 'Doctor',
    isPatient: user?.role === 'Patient',
  };
};

// Hook for loading states
export const useLoading = () => {
  const authLoading = useSelector((state: RootState) => state.auth.isLoading);
  const userLoading = useSelector((state: RootState) => state.users.isLoading);
  const appointmentLoading = useSelector((state: RootState) => state.appointments.isLoading);
  const bloodRequestLoading = useSelector((state: RootState) => state.bloodRequests.isLoading);
  const donationLoading = useSelector((state: RootState) => state.donations.isLoading);

  return {
    authLoading,
    userLoading,
    appointmentLoading,
    bloodRequestLoading,
    donationLoading,
    anyLoading: authLoading || userLoading || appointmentLoading || bloodRequestLoading || donationLoading,
  };
};

// Hook for error handling
export const useErrors = () => {
  const authError = useSelector((state: RootState) => state.auth.error);
  const userError = useSelector((state: RootState) => state.users.error);
  const appointmentError = useSelector((state: RootState) => state.appointments.error);
  const bloodRequestError = useSelector((state: RootState) => state.bloodRequests.error);
  const donationError = useSelector((state: RootState) => state.donations.error);

  const errors = [authError, userError, appointmentError, bloodRequestError, donationError]
    .filter(Boolean);

  return {
    errors,
    hasErrors: errors.length > 0,
    firstError: errors[0] || null,
  };
};
