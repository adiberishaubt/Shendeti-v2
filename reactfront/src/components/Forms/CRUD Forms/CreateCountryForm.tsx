import { useState } from 'react';
import SelectGroupTwo from '../SelectGroup/SelectGroupTwo'
import { Link, useNavigate } from 'react-router-dom'
import axios from 'axios';

const CreateCountryForm = () => {
    const [name, setName] = useState('');
    const [error, setError] = useState('');
    const navigate = useNavigate();

    const handleCreation = (e: React.FormEvent<HTMLFormElement>) => {
        e.preventDefault();
        axios.post('http://localhost:5095/Country', {
            'name': name
        })
        .then(res => navigate('/countries'))
        .catch(err => setError(err.response.data));
    }

  return (
    <div className="rounded-sm border border-stroke bg-white shadow-default dark:border-strokedark dark:bg-boxdark">
        <div className="border-b border-stroke py-4 px-6.5 dark:border-strokedark">
            <h3 className="font-medium text-black dark:text-white">
                Krijo Shtet
            </h3>
        </div>

        <form onSubmit={handleCreation}>
            <div className="flex flex-col gap-5.5 p-6.5">
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
            </div>

            {/* <div className="flex flex-col gap-5.5 p-6.5">
                <SelectGroupTwo />
            </div> */}
            
            <p className='text-red-500 text-center'>{error}</p>
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

export default CreateCountryForm