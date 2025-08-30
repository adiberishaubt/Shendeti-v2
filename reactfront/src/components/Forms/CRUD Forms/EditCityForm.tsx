import { FormEvent, useEffect, useState } from 'react';
import { useNavigate, useParams } from 'react-router-dom'
import axios from 'axios';
import { useAppDispatch, useAppSelector } from '../../../hooks/hooks';
import { getAllCountries } from '../../../slices/countrySlice';
import SelectGroupTwo from '../SelectGroup/SelectGroupTwo';

const EditCityForm = () => {
    const { id } = useParams();
    const [name, setName] = useState('');
    const [countryId, setCountryId] = useState('');
    const [latitude, setLatitude] = useState('');
    const [longitude, setLongitude] = useState('');
    const {countries} = useAppSelector(state => state.country);
    const [errors, setErrors] = useState(['']);
    const navigate = useNavigate();
    const dispatch = useAppDispatch();

    useEffect(() => {
        if(errors.length == 0) {
            axios.put(`http://localhost:5095/City/id?id=${id}`, {
              'name': name,
              "countryId": countryId,
              "latitude": parseFloat(latitude),
              "longitude": parseFloat(longitude)
            })
              .then(res => navigate('/cities'))
              .catch(err => setErrors([err.response.data]));
        }
      }, [errors]);

    useEffect(() => {
        dispatch(getAllCountries());

        axios
        .get(`http://localhost:5095/City/id?id=${id}`)
        .then(res => {
            setName(res.data.name),
            setCountryId(res.data.country.id),
            setLatitude(res.data.latitude),
            setLongitude(res.data.longitude)
        })
        .catch(e => setErrors([e.response.data]));
      }, []);

    const handleEdit = (e: FormEvent<HTMLFormElement>) => {
        e.preventDefault();

        const missingFields = [];
    
        if (name === '') {
        missingFields.push('Name field is missing');
        }

        if (latitude === '') {
        missingFields.push('Latitude field is missing');
        }

        if (longitude === '') {
        missingFields.push('Longitude field is missing');
        }

        if (countryId === '') {
        missingFields.push('Country field is missing');
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

        <form onSubmit={(e) => handleEdit(e)}>
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
                            value={name}
                            onChange={(e) => setName(e.target.value)}
                        />
                    </div>

                    <div>
                        <label className="mb-3 block text-black dark:text-white">
                            Longitude
                        </label>
                        <input
                            type="number"
                            placeholder="Shkruaj ketu..."
                            className="w-full rounded-lg border-[1.5px] border-stroke bg-transparent py-3 px-5 text-black outline-none transition focus:border-meta-3 active:border-meta-3 disabled:cursor-default disabled:bg-whiter dark:border-form-strokedark dark:bg-form-input dark:text-white dark:focus:border-meta-3"
                            value={longitude}
                            onChange={(e) => setLongitude(e.target.value)}
                        />
                    </div>

                    <div>
                        <label className="mb-3 block text-black dark:text-white">
                            Latitude
                        </label>
                        <input
                            type="number"
                            placeholder="Shkruaj ketu..."
                            className="w-full rounded-lg border-[1.5px] border-stroke bg-transparent py-3 px-5 text-black outline-none transition focus:border-meta-3 active:border-meta-3 disabled:cursor-default disabled:bg-whiter dark:border-form-strokedark dark:bg-form-input dark:text-white dark:focus:border-meta-3"
                            value={latitude}
                            onChange={(e) => setLatitude(e.target.value)}
                        />
                    </div>

                    <div>
                        <SelectGroupTwo title='Zgjedh Shtetin' selectedOption={countryId} setSelectedOption={setCountryId} data={countries} />
                    </div>

                </div>
            </div>
            
            {errors.map(e => <p className='text-red-500 block text-center'>{e}</p>)}
            <div className="flex flex-col gap-5.5 p-6.5">
                <button 
                    className="inline-flex items-center justify-center bg-meta-3 py-3 px-10 text-center font-medium text-white hover:bg-opacity-90 lg:px-8 xl:px-10">
                        Perditeso
                </button>
            </div>
        </form>

    </div>
  )
}

export default EditCityForm