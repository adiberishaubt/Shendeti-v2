import React from 'react';
import Card from '../components/ui/Card';

const Donations: React.FC = () => {
  return (
    <div className="space-y-6">
      <h1 className="text-2xl font-bold text-gray-900">Blood Donations</h1>
      <Card>
        <p className="text-gray-600">Blood donation tracking and management system.</p>
      </Card>
    </div>
  );
};

export default Donations;
