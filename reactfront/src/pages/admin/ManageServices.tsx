import React from 'react';
import Card from '../../components/ui/Card';

const ManageServices: React.FC = () => {
  return (
    <div className="space-y-6">
      <h1 className="text-2xl font-bold text-gray-900">Manage Services</h1>
      <Card>
        <p className="text-gray-600">Medical services management.</p>
      </Card>
    </div>
  );
};

export default ManageServices;
