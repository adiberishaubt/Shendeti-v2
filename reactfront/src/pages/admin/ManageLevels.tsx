import React from 'react';
import Card from '../../components/ui/Card';

const ManageLevels: React.FC = () => {
  return (
    <div className="space-y-6">
      <h1 className="text-2xl font-bold text-gray-900">Manage Levels</h1>
      <Card>
        <p className="text-gray-600">Education level management.</p>
      </Card>
    </div>
  );
};

export default ManageLevels;
