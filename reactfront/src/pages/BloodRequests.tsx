import React, { useEffect, useState } from 'react';
import { useDispatch, useSelector } from 'react-redux';
import { RootState } from '../store';
import { fetchBloodRequests, createBloodRequest } from '../store/slices/bloodRequestSlice';
import { fetchDonations, createDonation } from '../store/slices/donationSlice';
import Button from '../components/ui/Button';
import Card from '../components/ui/Card';
import { HeartIcon, ExclamationTriangleIcon, CheckCircleIcon } from '@heroicons/react/24/outline';

const BloodRequests: React.FC = () => {
  const dispatch = useDispatch();
  const { bloodRequests, isLoading } = useSelector((state: RootState) => state.bloodRequests);
  const { donations } = useSelector((state: RootState) => state.donations);
  const { user } = useSelector((state: RootState) => state.auth);

  const [showCreateForm, setShowCreateForm] = useState(false);
  const [bloodType, setBloodType] = useState('');
  const [urgency, setUrgency] = useState<'Low' | 'Medium' | 'High' | 'Critical'>('Medium');
  const [quantity, setQuantity] = useState(1);
  const [reason, setReason] = useState('');
  const [hospitalName, setHospitalName] = useState('');
  const [hospitalAddress, setHospitalAddress] = useState('');
  const [contactPerson, setContactPerson] = useState('');
  const [contactPhone, setContactPhone] = useState('');

  useEffect(() => {
    dispatch(fetchBloodRequests());
    dispatch(fetchDonations());
  }, [dispatch]);

  const handleCreateBloodRequest = async (e: React.FormEvent) => {
    e.preventDefault();
    if (!bloodType || !reason || !hospitalName) return;

    try {
      await dispatch(createBloodRequest({
        bloodType,
        urgency,
        quantity,
        reason,
        hospitalName,
        hospitalAddress,
        contactPerson,
        contactPhone,
      })).unwrap();
      
      setShowCreateForm(false);
      setBloodType('');
      setUrgency('Medium');
      setQuantity(1);
      setReason('');
      setHospitalName('');
      setHospitalAddress('');
      setContactPerson('');
      setContactPhone('');
    } catch (error) {
      console.error('Failed to create blood request:', error);
    }
  };

  const getUrgencyColor = (urgency: string) => {
    switch (urgency) {
      case 'Low': return 'bg-green-100 text-green-800';
      case 'Medium': return 'bg-yellow-100 text-yellow-800';
      case 'High': return 'bg-orange-100 text-orange-800';
      case 'Critical': return 'bg-red-100 text-red-800';
      default: return 'bg-gray-100 text-gray-800';
    }
  };

  const getStatusColor = (status: string) => {
    switch (status) {
      case 'Pending': return 'bg-yellow-100 text-yellow-800';
      case 'Accepted': return 'bg-blue-100 text-blue-800';
      case 'Completed': return 'bg-green-100 text-green-800';
      case 'Cancelled': return 'bg-red-100 text-red-800';
      default: return 'bg-gray-100 text-gray-800';
    }
  };

  return (
    <div className="space-y-6">
      <div className="flex justify-between items-center">
        <h1 className="text-2xl font-bold text-gray-900">Blood Requests</h1>
        <Button onClick={() => setShowCreateForm(true)}>
          Create Blood Request
        </Button>
      </div>

      {/* Create Blood Request Modal */}
      {showCreateForm && (
        <div className="fixed inset-0 bg-gray-600 bg-opacity-50 overflow-y-auto h-full w-full z-50">
          <div className="relative top-10 mx-auto p-5 border w-full max-w-2xl shadow-lg rounded-md bg-white">
            <div className="mt-3">
              <h3 className="text-lg font-medium text-gray-900 mb-4">Create Blood Request</h3>
              <form onSubmit={handleCreateBloodRequest} className="space-y-4">
                <div className="grid grid-cols-2 gap-4">
                  <div>
                    <label className="block text-sm font-medium text-gray-700 mb-1">
                      Blood Type Required
                    </label>
                    <select
                      value={bloodType}
                      onChange={(e) => setBloodType(e.target.value)}
                      className="input-field"
                      required
                    >
                      <option value="">Select blood type</option>
                      <option value="A+">A+</option>
                      <option value="A-">A-</option>
                      <option value="B+">B+</option>
                      <option value="B-">B-</option>
                      <option value="AB+">AB+</option>
                      <option value="AB-">AB-</option>
                      <option value="O+">O+</option>
                      <option value="O-">O-</option>
                    </select>
                  </div>

                  <div>
                    <label className="block text-sm font-medium text-gray-700 mb-1">
                      Urgency Level
                    </label>
                    <select
                      value={urgency}
                      onChange={(e) => setUrgency(e.target.value as any)}
                      className="input-field"
                    >
                      <option value="Low">Low</option>
                      <option value="Medium">Medium</option>
                      <option value="High">High</option>
                      <option value="Critical">Critical</option>
                    </select>
                  </div>
                </div>

                <div>
                  <label className="block text-sm font-medium text-gray-700 mb-1">
                    Quantity (Units)
                  </label>
                  <input
                    type="number"
                    min="1"
                    value={quantity}
                    onChange={(e) => setQuantity(parseInt(e.target.value))}
                    className="input-field"
                    required
                  />
                </div>

                <div>
                  <label className="block text-sm font-medium text-gray-700 mb-1">
                    Reason for Request
                  </label>
                  <textarea
                    value={reason}
                    onChange={(e) => setReason(e.target.value)}
                    className="input-field"
                    rows={3}
                    placeholder="Describe the medical condition or procedure requiring blood..."
                    required
                  />
                </div>

                <div className="grid grid-cols-2 gap-4">
                  <div>
                    <label className="block text-sm font-medium text-gray-700 mb-1">
                      Hospital Name
                    </label>
                    <input
                      type="text"
                      value={hospitalName}
                      onChange={(e) => setHospitalName(e.target.value)}
                      className="input-field"
                      required
                    />
                  </div>

                  <div>
                    <label className="block text-sm font-medium text-gray-700 mb-1">
                      Contact Person
                    </label>
                    <input
                      type="text"
                      value={contactPerson}
                      onChange={(e) => setContactPerson(e.target.value)}
                      className="input-field"
                      required
                    />
                  </div>
                </div>

                <div>
                  <label className="block text-sm font-medium text-gray-700 mb-1">
                    Hospital Address
                  </label>
                  <textarea
                    value={hospitalAddress}
                    onChange={(e) => setHospitalAddress(e.target.value)}
                    className="input-field"
                    rows={2}
                    required
                  />
                </div>

                <div>
                  <label className="block text-sm font-medium text-gray-700 mb-1">
                    Contact Phone
                  </label>
                  <input
                    type="tel"
                    value={contactPhone}
                    onChange={(e) => setContactPhone(e.target.value)}
                    className="input-field"
                    required
                  />
                </div>

                <div className="flex justify-end space-x-3">
                  <Button
                    type="button"
                    variant="outline"
                    onClick={() => setShowCreateForm(false)}
                  >
                    Cancel
                  </Button>
                  <Button type="submit" isLoading={isLoading}>
                    Create Request
                  </Button>
                </div>
              </form>
            </div>
          </div>
        </div>
      )}

      {/* Blood Requests List */}
      <div className="grid grid-cols-1 lg:grid-cols-2 gap-6">
        {bloodRequests.map((request) => (
          <Card key={request.id} className="p-6">
            <div className="flex items-start justify-between">
              <div className="flex items-center space-x-3">
                <div className="p-2 bg-red-100 rounded-lg">
                  <HeartIcon className="h-6 w-6 text-red-600" />
                </div>
                <div>
                  <h3 className="text-lg font-medium text-gray-900">
                    {request.bloodType} Blood Request
                  </h3>
                  <p className="text-sm text-gray-500">
                    {request.quantity} unit(s) needed
                  </p>
                </div>
              </div>
              <div className="flex flex-col space-y-1">
                <span className={`px-2 py-1 text-xs font-medium rounded-full ${getUrgencyColor(request.urgency)}`}>
                  {request.urgency}
                </span>
                <span className={`px-2 py-1 text-xs font-medium rounded-full ${getStatusColor(request.status)}`}>
                  {request.status}
                </span>
              </div>
            </div>

            <div className="mt-4 space-y-2">
              <div className="text-sm text-gray-600">
                <strong>Hospital:</strong> {request.hospitalName}
              </div>
              <div className="text-sm text-gray-600">
                <strong>Contact:</strong> {request.contactPerson} - {request.contactPhone}
              </div>
              <div className="text-sm text-gray-600">
                <strong>Reason:</strong> {request.reason}
              </div>
              <div className="text-sm text-gray-500">
                Created: {new Date(request.createdAt).toLocaleDateString()}
              </div>
            </div>

            <div className="mt-4 flex space-x-2">
              {request.status === 'Pending' && (
                <Button size="sm" variant="outline">
                  Find Donors
                </Button>
              )}
              {request.status === 'Accepted' && (
                <Button size="sm" variant="outline">
                  Track Progress
                </Button>
              )}
              {request.status === 'Completed' && (
                <Button size="sm" variant="outline">
                  View Details
                </Button>
              )}
            </div>
          </Card>
        ))}
      </div>

      {bloodRequests.length === 0 && (
        <Card className="text-center py-12">
          <HeartIcon className="mx-auto h-12 w-12 text-gray-400" />
          <h3 className="mt-2 text-sm font-medium text-gray-900">No blood requests</h3>
          <p className="mt-1 text-sm text-gray-500">
            Create your first blood request to get started.
          </p>
          <div className="mt-6">
            <Button onClick={() => setShowCreateForm(true)}>
              Create Blood Request
            </Button>
          </div>
        </Card>
      )}
    </div>
  );
};

export default BloodRequests;
