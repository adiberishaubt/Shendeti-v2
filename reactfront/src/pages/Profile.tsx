import React from 'react';
import Card from '../components/ui/Card';

const Profile: React.FC = () => {
  return (
    <div className="space-y-6">
      <h1 className="text-2xl font-bold text-gray-900">Profile</h1>
      <Card>
        <p className="text-gray-600">User profile management.</p>
      </Card>
    </div>
  );
};

export default Profile;
