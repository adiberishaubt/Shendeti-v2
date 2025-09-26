import React, { useEffect } from 'react';
import { useDispatch, useSelector } from 'react-redux';
import { RootState } from '../store';
import {
  CalendarIcon,
  HeartIcon,
  UserGroupIcon,
  ChartBarIcon,
} from '@heroicons/react/24/outline';
import {
  LineChart,
  Line,
  AreaChart,
  Area,
  XAxis,
  YAxis,
  CartesianGrid,
  Tooltip,
  ResponsiveContainer,
  PieChart,
  Pie,
  Cell,
  BarChart,
  Bar,
} from 'recharts';

const Dashboard: React.FC = () => {
  const dispatch = useDispatch();
  const { user } = useSelector((state: RootState) => state.auth);
  const { appointments } = useSelector((state: RootState) => state.appointments);
  const { bloodRequests } = useSelector((state: RootState) => state.bloodRequests);
  const { donations } = useSelector((state: RootState) => state.donations);

  // Sample data for charts
  const appointmentData = [
    { name: 'Jan', appointments: 45 },
    { name: 'Feb', appointments: 52 },
    { name: 'Mar', appointments: 48 },
    { name: 'Apr', appointments: 61 },
    { name: 'May', appointments: 55 },
    { name: 'Jun', appointments: 67 },
  ];

  const bloodTypeData = [
    { name: 'A+', value: 25, color: '#ef4444' },
    { name: 'A-', value: 15, color: '#f97316' },
    { name: 'B+', value: 20, color: '#eab308' },
    { name: 'B-', value: 10, color: '#22c55e' },
    { name: 'AB+', value: 8, color: '#06b6d4' },
    { name: 'AB-', value: 5, color: '#8b5cf6' },
    { name: 'O+', value: 30, color: '#ec4899' },
    { name: 'O-', value: 12, color: '#f43f5e' },
  ];

  const urgencyData = [
    { name: 'Low', value: 15, color: '#22c55e' },
    { name: 'Medium', value: 25, color: '#eab308' },
    { name: 'High', value: 35, color: '#f97316' },
    { name: 'Critical', value: 25, color: '#ef4444' },
  ];

  const stats = [
    {
      name: 'Total Appointments',
      value: appointments.length,
      icon: CalendarIcon,
      color: 'bg-blue-500',
      change: '+12%',
      changeType: 'positive',
    },
    {
      name: 'Blood Requests',
      value: bloodRequests.length,
      icon: HeartIcon,
      color: 'bg-red-500',
      change: '+8%',
      changeType: 'positive',
    },
    {
      name: 'Donations',
      value: donations.length,
      icon: HeartIcon,
      color: 'bg-green-500',
      change: '+15%',
      changeType: 'positive',
    },
    {
      name: 'Active Users',
      value: 1247,
      icon: UserGroupIcon,
      color: 'bg-purple-500',
      change: '+5%',
      changeType: 'positive',
    },
  ];

  return (
    <div className="space-y-6">
      {/* Welcome Section */}
      <div className="bg-white rounded-lg shadow p-6">
        <h1 className="text-2xl font-bold text-gray-900">
          Welcome back, {user?.firstName}!
        </h1>
        <p className="text-gray-600 mt-2">
          Here's what's happening with your blood donation management system today.
        </p>
      </div>

      {/* Stats Grid */}
      <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-4 gap-6">
        {stats.map((stat) => (
          <div key={stat.name} className="bg-white rounded-lg shadow p-6">
            <div className="flex items-center">
              <div className={`p-3 rounded-md ${stat.color}`}>
                <stat.icon className="h-6 w-6 text-white" />
              </div>
              <div className="ml-4">
                <p className="text-sm font-medium text-gray-500">{stat.name}</p>
                <p className="text-2xl font-semibold text-gray-900">{stat.value}</p>
              </div>
            </div>
            <div className="mt-4">
              <span className={`text-sm font-medium ${
                stat.changeType === 'positive' ? 'text-green-600' : 'text-red-600'
              }`}>
                {stat.change}
              </span>
              <span className="text-sm text-gray-500 ml-2">from last month</span>
            </div>
          </div>
        ))}
      </div>

      {/* Charts Grid */}
      <div className="grid grid-cols-1 lg:grid-cols-2 gap-6">
        {/* Appointments Trend */}
        <div className="bg-white rounded-lg shadow p-6">
          <h3 className="text-lg font-medium text-gray-900 mb-4">Appointments Trend</h3>
          <ResponsiveContainer width="100%" height={300}>
            <LineChart data={appointmentData}>
              <CartesianGrid strokeDasharray="3 3" />
              <XAxis dataKey="name" />
              <YAxis />
              <Tooltip />
              <Line type="monotone" dataKey="appointments" stroke="#3b82f6" strokeWidth={2} />
            </LineChart>
          </ResponsiveContainer>
        </div>

        {/* Blood Type Distribution */}
        <div className="bg-white rounded-lg shadow p-6">
          <h3 className="text-lg font-medium text-gray-900 mb-4">Blood Type Distribution</h3>
          <ResponsiveContainer width="100%" height={300}>
            <PieChart>
              <Pie
                data={bloodTypeData}
                cx="50%"
                cy="50%"
                labelLine={false}
                label={({ name, percent }) => `${name} ${(percent * 100).toFixed(0)}%`}
                outerRadius={80}
                fill="#8884d8"
                dataKey="value"
              >
                {bloodTypeData.map((entry, index) => (
                  <Cell key={`cell-${index}`} fill={entry.color} />
                ))}
              </Pie>
              <Tooltip />
            </PieChart>
          </ResponsiveContainer>
        </div>

        {/* Blood Requests by Urgency */}
        <div className="bg-white rounded-lg shadow p-6">
          <h3 className="text-lg font-medium text-gray-900 mb-4">Blood Requests by Urgency</h3>
          <ResponsiveContainer width="100%" height={300}>
            <BarChart data={urgencyData}>
              <CartesianGrid strokeDasharray="3 3" />
              <XAxis dataKey="name" />
              <YAxis />
              <Tooltip />
              <Bar dataKey="value" fill="#ef4444" />
            </BarChart>
          </ResponsiveContainer>
        </div>

        {/* Donation Activity */}
        <div className="bg-white rounded-lg shadow p-6">
          <h3 className="text-lg font-medium text-gray-900 mb-4">Donation Activity</h3>
          <ResponsiveContainer width="100%" height={300}>
            <AreaChart data={appointmentData}>
              <CartesianGrid strokeDasharray="3 3" />
              <XAxis dataKey="name" />
              <YAxis />
              <Tooltip />
              <Area type="monotone" dataKey="appointments" stroke="#22c55e" fill="#22c55e" fillOpacity={0.3} />
            </AreaChart>
          </ResponsiveContainer>
        </div>
      </div>

      {/* Recent Activity */}
      <div className="bg-white rounded-lg shadow">
        <div className="px-6 py-4 border-b border-gray-200">
          <h3 className="text-lg font-medium text-gray-900">Recent Activity</h3>
        </div>
        <div className="p-6">
          <div className="space-y-4">
            <div className="flex items-center space-x-3">
              <div className="flex-shrink-0">
                <CalendarIcon className="h-5 w-5 text-blue-500" />
              </div>
              <div className="flex-1 min-w-0">
                <p className="text-sm text-gray-900">New appointment scheduled</p>
                <p className="text-sm text-gray-500">Dr. Smith - Cardiology</p>
              </div>
              <div className="text-sm text-gray-500">2 hours ago</div>
            </div>
            <div className="flex items-center space-x-3">
              <div className="flex-shrink-0">
                <HeartIcon className="h-5 w-5 text-red-500" />
              </div>
              <div className="flex-1 min-w-0">
                <p className="text-sm text-gray-900">Blood request created</p>
                <p className="text-sm text-gray-500">O+ blood needed urgently</p>
              </div>
              <div className="text-sm text-gray-500">4 hours ago</div>
            </div>
            <div className="flex items-center space-x-3">
              <div className="flex-shrink-0">
                <HeartIcon className="h-5 w-5 text-green-500" />
              </div>
              <div className="flex-1 min-w-0">
                <p className="text-sm text-gray-900">Blood donation completed</p>
                <p className="text-sm text-gray-500">A+ blood donated</p>
              </div>
              <div className="text-sm text-gray-500">6 hours ago</div>
            </div>
          </div>
        </div>
      </div>
    </div>
  );
};

export default Dashboard;
