import React from 'react';
import { Link, useLocation } from 'react-router-dom';
import { useSelector } from 'react-redux';
import { RootState } from '../../store';
import {
  HomeIcon,
  CalendarIcon,
  HeartIcon,
  UserGroupIcon,
  UserIcon,
  CogIcon,
  ShieldCheckIcon,
  BuildingOfficeIcon,
  MapIcon,
  AcademicCapIcon,
  ChartBarIcon,
  ClipboardDocumentListIcon,
} from '@heroicons/react/24/outline';

const Sidebar: React.FC = () => {
  const location = useLocation();
  const { user } = useSelector((state: RootState) => state.auth);

  const navigation = [
    { name: 'Dashboard', href: '/dashboard', icon: HomeIcon },
    { name: 'Appointments', href: '/appointments', icon: CalendarIcon },
    { name: 'Blood Requests', href: '/blood-requests', icon: HeartIcon },
    { name: 'Donations', href: '/donations', icon: HeartIcon },
    { name: 'Doctors', href: '/doctors', icon: UserGroupIcon },
    { name: 'Patients', href: '/patients', icon: UserIcon },
    { name: 'Profile', href: '/profile', icon: UserIcon },
    { name: 'Settings', href: '/settings', icon: CogIcon },
  ];

  const adminNavigation = [
    { name: 'Admin Dashboard', href: '/admin/dashboard', icon: ShieldCheckIcon },
    { name: 'Manage Users', href: '/admin/users', icon: UserGroupIcon },
    { name: 'Specializations', href: '/admin/specializations', icon: AcademicCapIcon },
    { name: 'Countries', href: '/admin/countries', icon: MapIcon },
    { name: 'Cities', href: '/admin/cities', icon: BuildingOfficeIcon },
    { name: 'Services', href: '/admin/services', icon: ClipboardDocumentListIcon },
    { name: 'Levels', href: '/admin/levels', icon: ChartBarIcon },
    { name: 'Educations', href: '/admin/educations', icon: AcademicCapIcon },
  ];

  const isActive = (href: string) => {
    return location.pathname === href;
  };

  return (
    <div className="hidden md:flex md:w-64 md:flex-col">
      <div className="flex flex-col flex-grow pt-5 bg-white overflow-y-auto border-r border-gray-200">
        <div className="flex items-center flex-shrink-0 px-4">
          <h1 className="text-xl font-bold text-primary-600">Shendeti</h1>
        </div>
        <div className="mt-5 flex-grow flex flex-col">
          <nav className="flex-1 px-2 space-y-1">
            {/* Main Navigation */}
            {navigation.map((item) => (
              <Link
                key={item.name}
                to={item.href}
                className={`${
                  isActive(item.href)
                    ? 'bg-primary-100 text-primary-900'
                    : 'text-gray-600 hover:bg-gray-50 hover:text-gray-900'
                } group flex items-center px-2 py-2 text-sm font-medium rounded-md`}
              >
                <item.icon
                  className={`${
                    isActive(item.href) ? 'text-primary-500' : 'text-gray-400 group-hover:text-gray-500'
                  } mr-3 flex-shrink-0 h-6 w-6`}
                />
                {item.name}
              </Link>
            ))}

            {/* Admin Navigation */}
            {user?.role === 'Admin' && (
              <>
                <div className="border-t border-gray-200 my-4"></div>
                <div className="px-2 py-1">
                  <h3 className="text-xs font-semibold text-gray-500 uppercase tracking-wider">
                    Administration
                  </h3>
                </div>
                {adminNavigation.map((item) => (
                  <Link
                    key={item.name}
                    to={item.href}
                    className={`${
                      isActive(item.href)
                        ? 'bg-primary-100 text-primary-900'
                        : 'text-gray-600 hover:bg-gray-50 hover:text-gray-900'
                    } group flex items-center px-2 py-2 text-sm font-medium rounded-md`}
                  >
                    <item.icon
                      className={`${
                        isActive(item.href) ? 'text-primary-500' : 'text-gray-400 group-hover:text-gray-500'
                      } mr-3 flex-shrink-0 h-6 w-6`}
                    />
                    {item.name}
                  </Link>
                ))}
              </>
            )}
          </nav>
        </div>
      </div>
    </div>
  );
};

export default Sidebar;
