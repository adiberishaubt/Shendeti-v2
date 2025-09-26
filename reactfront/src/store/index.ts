import { configureStore } from '@reduxjs/toolkit';
import authSlice from './slices/authSlice';
import userSlice from './slices/userSlice';
import appointmentSlice from './slices/appointmentSlice';
import bloodRequestSlice from './slices/bloodRequestSlice';
import donationSlice from './slices/donationSlice';
import feedbackSlice from './slices/feedbackSlice';
import countrySlice from './slices/countrySlice';
import citySlice from './slices/citySlice';
import specializationSlice from './slices/specializationSlice';
import educationSlice from './slices/educationSlice';
import levelSlice from './slices/levelSlice';
import serviceSlice from './slices/serviceSlice';
import scheduleSlice from './slices/scheduleSlice';
import slotSlice from './slices/slotSlice';

export const store = configureStore({
  reducer: {
    auth: authSlice,
    users: userSlice,
    appointments: appointmentSlice,
    bloodRequests: bloodRequestSlice,
    donations: donationSlice,
    feedback: feedbackSlice,
    countries: countrySlice,
    cities: citySlice,
    specializations: specializationSlice,
    educations: educationSlice,
    levels: levelSlice,
    services: serviceSlice,
    schedules: scheduleSlice,
    slots: slotSlice,
  },
  middleware: (getDefaultMiddleware) =>
    getDefaultMiddleware({
      serializableCheck: {
        ignoredActions: ['persist/PERSIST'],
      },
    }),
});

export type RootState = ReturnType<typeof store.getState>;
export type AppDispatch = typeof store.dispatch;
