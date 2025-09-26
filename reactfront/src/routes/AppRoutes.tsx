import React from 'react';
import { BrowserRouter as Router, Routes, Route, Navigate } from 'react-router-dom';
import { useSelector } from 'react-redux';
import { RootState } from '../store';

// Layout Components
import Layout from '../components/layout/Layout';
import AuthLayout from '../components/layout/AuthLayout';

// Pages
import Login from '../pages/auth/Login';
import Register from '../pages/auth/Register';
import Dashboard from '../pages/Dashboard';
import Appointments from '../pages/Appointments';
import BloodRequests from '../pages/BloodRequests';
import Donations from '../pages/Donations';
import Doctors from '../pages/Doctors';
import Patients from '../pages/Patients';
import Profile from '../pages/Profile';
import Settings from '../pages/Settings';

// Admin Pages
import AdminDashboard from '../pages/admin/AdminDashboard';
import ManageUsers from '../pages/admin/ManageUsers';
import ManageSpecializations from '../pages/admin/ManageSpecializations';
import ManageCountries from '../pages/admin/ManageCountries';
import ManageCities from '../pages/admin/ManageCities';
import ManageServices from '../pages/admin/ManageServices';
import ManageLevels from '../pages/admin/ManageLevels';
import ManageEducations from '../pages/admin/ManageEducations';

// Protected Route Component
const ProtectedRoute: React.FC<{ children: React.ReactNode; adminOnly?: boolean }> = ({ 
  children, 
  adminOnly = false 
}) => {
  const { isAuthenticated, user } = useSelector((state: RootState) => state.auth);
  
  if (!isAuthenticated) {
    return <Navigate to="/login" replace />;
  }
  
  if (adminOnly && user?.role !== 'Admin') {
    return <Navigate to="/dashboard" replace />;
  }
  
  return <>{children}</>;
};

const AppRoutes: React.FC = () => {
  const { isAuthenticated } = useSelector((state: RootState) => state.auth);

  return (
    <Router>
      <Routes>
        {/* Public Routes */}
        <Route path="/login" element={
          <AuthLayout>
            <Login />
          </AuthLayout>
        } />
        <Route path="/register" element={
          <AuthLayout>
            <Register />
          </AuthLayout>
        } />

        {/* Protected Routes */}
        <Route path="/" element={
          <ProtectedRoute>
            <Layout />
          </ProtectedRoute>
        }>
          <Route index element={<Navigate to="/dashboard" replace />} />
          <Route path="dashboard" element={<Dashboard />} />
          <Route path="appointments" element={<Appointments />} />
          <Route path="blood-requests" element={<BloodRequests />} />
          <Route path="donations" element={<Donations />} />
          <Route path="doctors" element={<Doctors />} />
          <Route path="patients" element={<Patients />} />
          <Route path="profile" element={<Profile />} />
          <Route path="settings" element={<Settings />} />
        </Route>

        {/* Admin Routes */}
        <Route path="/admin" element={
          <ProtectedRoute adminOnly>
            <Layout />
          </ProtectedRoute>
        }>
          <Route index element={<Navigate to="/admin/dashboard" replace />} />
          <Route path="dashboard" element={<AdminDashboard />} />
          <Route path="users" element={<ManageUsers />} />
          <Route path="specializations" element={<ManageSpecializations />} />
          <Route path="countries" element={<ManageCountries />} />
          <Route path="cities" element={<ManageCities />} />
          <Route path="services" element={<ManageServices />} />
          <Route path="levels" element={<ManageLevels />} />
          <Route path="educations" element={<ManageEducations />} />
        </Route>

        {/* Catch all route */}
        <Route path="*" element={<Navigate to="/dashboard" replace />} />
      </Routes>
    </Router>
  );
};

export default AppRoutes;
