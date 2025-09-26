import React from 'react';
import Card from '../components/ui/Card';

const Settings: React.FC = () => {
  return (
    <div className="space-y-6">
      <h1 className="text-2xl font-bold text-gray-900">Settings</h1>
      <Card>
        <p className="text-gray-600">Application settings and preferences.</p>
      </Card>
    </div>
  );
};

export default Settings;
