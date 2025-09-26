import React from 'react';
import Card from '../../components/ui/Card';

const ManageUsers: React.FC = () => {
  return (
    <div className="space-y-6">
      <h1 className="text-2xl font-bold text-gray-900">Manage Users</h1>
      <Card>
        <p className="text-gray-600">User management system for administrators.</p>
      </Card>
    </div>
  );
};

export default ManageUsers;
