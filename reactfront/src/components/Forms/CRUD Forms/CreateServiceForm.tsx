import { FormEvent, useEffect, useState } from 'react';
import { useNavigate } from 'react-router-dom'
import axios from 'axios';
import { useAppDispatch, useAppSelector } from '../../../hooks/hooks';
import SelectGroupTwo from '../SelectGroup/SelectGroupTwo';
import { getAllSpecializations } from '../../../slices/specializationSlice';

const CreateServiceForm = () => {
    const [name, setName] = useState('');
    const [specializationId, setSpecializationId] = useState('');
    const {specializations} = useAppSelector(state => state.specialization);
    const [errors, setErrors] = useState(['']);
    const navigate = useNavigate();
    const dispatch = useAppDispatch();

    useEffect(() => {
      dispatch(getAllSpecializations());
    }, [])

    useEffect(() => {
        if(errors.length == 0) {
          axios.post('http://localhost:5095/Service', {
              'name': name,
              "specializationId": specializationId
          })
          .then(res => navigate('/services'))
          .catch(err => {
            if(err.response.status == 422) {
              setErrors([err.response.data]);
            } else {
              setErrors([err.response.data.title]);
            }
          });
        }
  
    }, [errors]);
    
    const handleCreation = (e: FormEvent<HTMLFormElement>) => {
        e.preventDefault();

        const missingFields = [];
    
        if (name === '') {
            missingFields.push('Name field is missing');
        }

        if (specializationId === '') {
            missingFields.push('Specialization field is missing');
        }
    
        setErrors(missingFields);
  }

  return (
    <div className="rounded-sm border border-stroke bg-white shadow-default dark:border-strokedark dark:bg-boxdark">
        <div className="border-b border-stroke py-4 px-6.5 dark:border-strokedark">
            <h3 className="font-medium text-black dark:text-white">
                Krijo Shtet
            </h3>
        </div>

        <form onSubmit={(e) => handleCreation(e)}>
            <div className="flex flex-col gap-5.5 p-6.5">
                <div className='flex flex-col gap-4'>

                    <div>
                        <label className="mb-3 block text-black dark:text-white">
                            Emri
                        </label>
                        <input
                            type="text"
                            placeholder="Shkruaj ketu..."
                            className="w-full rounded-lg border-[1.5px] border-stroke bg-transparent py-3 px-5 text-black outline-none transition focus:border-meta-3 active:border-meta-3 disabled:cursor-default disabled:bg-whiter dark:border-form-strokedark dark:bg-form-input dark:text-white dark:focus:border-meta-3"
                            onChange={(e) => setName(e.target.value)}
                        />
                    </div>

                    <div>
                        <SelectGroupTwo title='Zgjedh Specializimin' selectedOption={specializationId} setSelectedOption={setSpecializationId} data={specializations} />
                    </div>
                </div>
            </div>
            
            {errors.map(e => <p className='text-red-500 block text-center'>{e}</p>)}
            <div className="flex flex-col gap-5.5 p-6.5">
                <button 
                    className="inline-flex items-center justify-center bg-meta-3 py-3 px-10 text-center font-medium text-white hover:bg-opacity-90 lg:px-8 xl:px-10">
                        Krijo
                </button>
            </div>
        </form>

    </div>
  )
}

export default CreateServiceForm