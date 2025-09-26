import React, { useEffect, useState } from 'react';
import { useDispatch, useSelector } from 'react-redux';
import { RootState } from '../store';
import { fetchAppointments, createAppointment } from '../store/slices/appointmentSlice';
import { fetchDoctors } from '../store/slices/userSlice';
import { fetchSlots } from '../store/slices/slotSlice';
import Button from '../components/ui/Button';
import Card from '../components/ui/Card';
import { CalendarIcon, ClockIcon, UserIcon } from '@heroicons/react/24/outline';

const Appointments: React.FC = () => {
  const dispatch = useDispatch();
  const { appointments, isLoading } = useSelector((state: RootState) => state.appointments);
  const { doctors } = useSelector((state: RootState) => state.users);
  const { slots } = useSelector((state: RootState) => state.slots);
  const { user } = useSelector((state: RootState) => state.auth);

  const [showCreateForm, setShowCreateForm] = useState(false);
  const [selectedDoctor, setSelectedDoctor] = useState('');
  const [selectedSlot, setSelectedSlot] = useState('');
  const [appointmentDate, setAppointmentDate] = useState('');
  const [notes, setNotes] = useState('');
  const [meetingType, setMeetingType] = useState<'InPerson' | 'Online'>('InPerson');

  useEffect(() => {
    dispatch(fetchAppointments());
    dispatch(fetchDoctors());
    dispatch(fetchSlots());
  }, [dispatch]);

  const handleCreateAppointment = async (e: React.FormEvent) => {
    e.preventDefault();
    if (!selectedDoctor || !selectedSlot || !appointmentDate) return;

    try {
      await dispatch(createAppointment({
        patientId: user?.id,
        doctorId: selectedDoctor,
        slotId: parseInt(selectedSlot),
        appointmentDate,
        notes,
        meetingType,
      })).unwrap();
      
      setShowCreateForm(false);
      setSelectedDoctor('');
      setSelectedSlot('');
      setAppointmentDate('');
      setNotes('');
      setMeetingType('InPerson');
    } catch (error) {
      console.error('Failed to create appointment:', error);
    }
  };

  const filteredSlots = slots.filter(slot => 
    slot.schedule?.doctorId === selectedDoctor && slot.isAvailable
  );

  return (
    <div className="space-y-6">
      <div className="flex justify-between items-center">
        <h1 className="text-2xl font-bold text-gray-900">Appointments</h1>
        <Button onClick={() => setShowCreateForm(true)}>
          Schedule Appointment
        </Button>
      </div>

      {/* Create Appointment Modal */}
      {showCreateForm && (
        <div className="fixed inset-0 bg-gray-600 bg-opacity-50 overflow-y-auto h-full w-full z-50">
          <div className="relative top-20 mx-auto p-5 border w-96 shadow-lg rounded-md bg-white">
            <div className="mt-3">
              <h3 className="text-lg font-medium text-gray-900 mb-4">Schedule New Appointment</h3>
              <form onSubmit={handleCreateAppointment} className="space-y-4">
                <div>
                  <label className="block text-sm font-medium text-gray-700 mb-1">
                    Select Doctor
                  </label>
                  <select
                    value={selectedDoctor}
                    onChange={(e) => setSelectedDoctor(e.target.value)}
                    className="input-field"
                    required
                  >
                    <option value="">Choose a doctor</option>
                    {doctors.map((doctor) => (
                      <option key={doctor.id} value={doctor.id}>
                        Dr. {doctor.firstName} {doctor.lastName} - {doctor.specialization?.name}
                      </option>
                    ))}
                  </select>
                </div>

                <div>
                  <label className="block text-sm font-medium text-gray-700 mb-1">
                    Appointment Date
                  </label>
                  <input
                    type="date"
                    value={appointmentDate}
                    onChange={(e) => setAppointmentDate(e.target.value)}
                    className="input-field"
                    required
                    min={new Date().toISOString().split('T')[0]}
                  />
                </div>

                <div>
                  <label className="block text-sm font-medium text-gray-700 mb-1">
                    Available Time Slots
                  </label>
                  <select
                    value={selectedSlot}
                    onChange={(e) => setSelectedSlot(e.target.value)}
                    className="input-field"
                    required
                    disabled={!selectedDoctor}
                  >
                    <option value="">Choose a time slot</option>
                    {filteredSlots.map((slot) => (
                      <option key={slot.id} value={slot.id}>
                        {slot.startTime} - {slot.endTime}
                      </option>
                    ))}
                  </select>
                </div>

                <div>
                  <label className="block text-sm font-medium text-gray-700 mb-1">
                    Meeting Type
                  </label>
                  <select
                    value={meetingType}
                    onChange={(e) => setMeetingType(e.target.value as 'InPerson' | 'Online')}
                    className="input-field"
                  >
                    <option value="InPerson">In Person</option>
                    <option value="Online">Online</option>
                  </select>
                </div>

                <div>
                  <label className="block text-sm font-medium text-gray-700 mb-1">
                    Notes (Optional)
                  </label>
                  <textarea
                    value={notes}
                    onChange={(e) => setNotes(e.target.value)}
                    className="input-field"
                    rows={3}
                    placeholder="Any additional notes..."
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
                    Schedule Appointment
                  </Button>
                </div>
              </form>
            </div>
          </div>
        </div>
      )}

      {/* Appointments List */}
      <div className="grid grid-cols-1 lg:grid-cols-2 gap-6">
        {appointments.map((appointment) => (
          <Card key={appointment.id} className="p-6">
            <div className="flex items-start justify-between">
              <div className="flex items-center space-x-3">
                <div className="p-2 bg-blue-100 rounded-lg">
                  <CalendarIcon className="h-6 w-6 text-blue-600" />
                </div>
                <div>
                  <h3 className="text-lg font-medium text-gray-900">
                    Dr. {appointment.doctor?.firstName} {appointment.doctor?.lastName}
                  </h3>
                  <p className="text-sm text-gray-500">
                    {appointment.doctor?.specialization?.name}
                  </p>
                </div>
              </div>
              <span className={`px-2 py-1 text-xs font-medium rounded-full ${
                appointment.status === 'Scheduled' ? 'bg-blue-100 text-blue-800' :
                appointment.status === 'Completed' ? 'bg-green-100 text-green-800' :
                appointment.status === 'Cancelled' ? 'bg-red-100 text-red-800' :
                'bg-yellow-100 text-yellow-800'
              }`}>
                {appointment.status}
              </span>
            </div>

            <div className="mt-4 space-y-2">
              <div className="flex items-center text-sm text-gray-600">
                <ClockIcon className="h-4 w-4 mr-2" />
                {new Date(appointment.appointmentDate).toLocaleDateString()} at {appointment.slot?.startTime}
              </div>
              <div className="flex items-center text-sm text-gray-600">
                <UserIcon className="h-4 w-4 mr-2" />
                {appointment.meetingType === 'InPerson' ? 'In Person' : 'Online'}
              </div>
              {appointment.notes && (
                <div className="text-sm text-gray-600">
                  <strong>Notes:</strong> {appointment.notes}
                </div>
              )}
            </div>

            <div className="mt-4 flex space-x-2">
              {appointment.status === 'Scheduled' && (
                <>
                  <Button size="sm" variant="outline">
                    Reschedule
                  </Button>
                  <Button size="sm" variant="danger">
                    Cancel
                  </Button>
                </>
              )}
              {appointment.status === 'Completed' && (
                <Button size="sm" variant="outline">
                  Leave Feedback
                </Button>
              )}
            </div>
          </Card>
        ))}
      </div>

      {appointments.length === 0 && (
        <Card className="text-center py-12">
          <CalendarIcon className="mx-auto h-12 w-12 text-gray-400" />
          <h3 className="mt-2 text-sm font-medium text-gray-900">No appointments</h3>
          <p className="mt-1 text-sm text-gray-500">
            Get started by scheduling your first appointment.
          </p>
          <div className="mt-6">
            <Button onClick={() => setShowCreateForm(true)}>
              Schedule Appointment
            </Button>
          </div>
        </Card>
      )}
    </div>
  );
};

export default Appointments;
