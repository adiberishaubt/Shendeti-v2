import { useEffect } from 'react';
import Breadcrumb from '../components/Breadcrumbs/Breadcrumb';
import { useAppDispatch, useAppSelector } from '../hooks/hooks';
import DefaultLayout from '../layout/DefaultLayout';
import Loader from '../common/Loader';
import { getPaginatedSpecializations } from '../slices/specializationSlice';
import SpecializationTable from '../components/Tables/SpecializationTable';

const Specializations = () => {
  const { isLoading } = useAppSelector(state => state.specialization);
  const dispatch = useAppDispatch();
  
  useEffect(() => {
    dispatch(getPaginatedSpecializations(1));
  }, []);

  if(isLoading) {
    return <Loader />
  }

  return (
    <DefaultLayout>
      <Breadcrumb pageName="Specializimet" />

      <div className="flex flex-col gap-10">
        <SpecializationTable />
      </div>
    </DefaultLayout>
  );
};

export default Specializations;
