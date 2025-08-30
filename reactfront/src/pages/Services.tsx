import { useEffect } from 'react';
import Breadcrumb from '../components/Breadcrumbs/Breadcrumb';
import { useAppDispatch, useAppSelector } from '../hooks/hooks';
import DefaultLayout from '../layout/DefaultLayout';
import Loader from '../common/Loader';
import { getPaginatedServices } from '../slices/serviceSlice';
import ServiceTable from '../components/Tables/ServiceTable';

const Services = () => {
  const { isLoading } = useAppSelector(state => state.service);
  const dispatch = useAppDispatch();
  
  
  useEffect(() => { 
    dispatch(getPaginatedServices(1));
  }, []);

  if(isLoading) {
    return <Loader />
  }

  return (
    <DefaultLayout>
      <Breadcrumb pageName="Sherbimet" />

      <div className="flex flex-col gap-10">
        <ServiceTable />
      </div>
    </DefaultLayout>
  );
};

export default Services;
