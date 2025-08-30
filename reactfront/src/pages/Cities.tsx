import { useEffect } from 'react';
import Breadcrumb from '../components/Breadcrumbs/Breadcrumb';
import { useAppDispatch, useAppSelector } from '../hooks/hooks';
import DefaultLayout from '../layout/DefaultLayout';
import Loader from '../common/Loader';
import { getPaginatedCities } from '../slices/citySlice';
import CityTable from '../components/Tables/CityTable';

const Cities = () => {
  const { isLoading } = useAppSelector(state => state.city);
  const dispatch = useAppDispatch();
  
  useEffect(() => {
    dispatch(getPaginatedCities(1));
  }, []);

  if(isLoading) {
    return <Loader />
  }

  return (
    <DefaultLayout>
      <Breadcrumb pageName="Qytetet" />

      <div className="flex flex-col gap-10">
        <CityTable />
      </div>
    </DefaultLayout>
  );
};

export default Cities;
