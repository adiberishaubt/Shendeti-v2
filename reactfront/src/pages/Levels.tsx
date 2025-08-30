import { useEffect } from 'react';
import Breadcrumb from '../components/Breadcrumbs/Breadcrumb';
import { useAppDispatch, useAppSelector } from '../hooks/hooks';
import DefaultLayout from '../layout/DefaultLayout';
import Loader from '../common/Loader';
import { getPaginatedLevels } from '../slices/levelSlice';
import LevelTable from '../components/Tables/LevelTable';

const Levels = () => {
  const { isLoading } = useAppSelector(state => state.level);
  const dispatch = useAppDispatch();
  
  useEffect(() => {
    dispatch(getPaginatedLevels(1));
  }, []);

  if(isLoading) {
    return <Loader />
  }

  return (
    <DefaultLayout>
      <Breadcrumb pageName="Levelet" />

      <div className="flex flex-col gap-10">
        <LevelTable />
      </div>
    </DefaultLayout>
  );
};

export default Levels;
