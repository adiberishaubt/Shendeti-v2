import { useEffect } from 'react';
import Breadcrumb from '../components/Breadcrumbs/Breadcrumb';
import CountryTable from '../components/Tables/CountryTable';
import { useAppDispatch, useAppSelector } from '../hooks/hooks';
import DefaultLayout from '../layout/DefaultLayout';
import { getAllCountries } from '../slices/countrySlice';
import Loader from '../common/Loader';

const Countries = () => {
  const { isLoading } = useAppSelector(state => state.country);
  const dispatch = useAppDispatch();
  
  useEffect(() => {
    dispatch(getAllCountries());
  }, []);

  if(isLoading) {
    return <Loader />
  }

  return (
    <DefaultLayout>
      <Breadcrumb pageName="Shtetet" />

      <div className="flex flex-col gap-10">
        <CountryTable />
      </div>
    </DefaultLayout>
  );
};

export default Countries;
