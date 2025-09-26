import React from 'react';
import Card from '../../components/ui/Card';

const ManageCities: React.FC = () => {
  return (
    <div className="space-y-6">
      <h1 className="text-2xl font-bold text-gray-900">Manage Cities</h1>
      <Card>
        <p className="text-gray-600">City management system.</p>
      </Card>
    </div>
  );
};

export default ManageCities;
