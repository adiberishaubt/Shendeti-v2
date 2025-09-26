import React from 'react';
import Card from '../components/ui/Card';

const Patients: React.FC = () => {
  return (
    <div className="space-y-6">
      <h1 className="text-2xl font-bold text-gray-900">Patients</h1>
      <Card>
        <p className="text-gray-600">Patient management system.</p>
      </Card>
    </div>
  );
};

export default Patients;
