import { ReactNode, useEffect, useState } from 'react';
import { Navigate, Outlet, Route, Routes, useLocation } from 'react-router-dom';
import Loader from './common/Loader';
import PageTitle from './components/PageTitle';
import SignIn from './pages/Authentication/SignIn';
import SignUp from './pages/Authentication/SignUp';
import Settings from './pages/Settings';
import Countries from './pages/Countries';
import CreateCountry from './pages/CRUDs/CreateCountry';
import EditCountry from './pages/CRUDs/EditCountry';
import { useAppSelector } from './hooks/hooks';
import { UserLoader } from './components/UserLoader';
import Cities from './pages/Cities';
import CreateCity from './pages/CRUDs/CreateCity';
import EditCity from './pages/CRUDs/EditCity';
import Levels from './pages/Levels';
import CreateLevel from './pages/CRUDs/CreateLevel';
import EditLevel from './pages/CRUDs/EditLevel';
import Specializations from './pages/Specializations';
import CreateSpecialization from './pages/CRUDs/CreateSpecialization';
import EditSpecialization from './pages/CRUDs/EditSpecialization';
import Services from './pages/Services';
import CreateService from './pages/CRUDs/CreateService';
import EditService from './pages/CRUDs/EditService';
import Homepage from './pages/Homepage';

function App() {
  const { user, isLoggedIn } = useAppSelector(state => state.user);
  const [loading, setLoading] = useState<boolean>(true);
  const { pathname } = useLocation();

  useEffect(() => {
    window.scrollTo(0, 0);
  }, [pathname]);

  useEffect(() => {
    setTimeout(() => setLoading(false), 1000);
  }, []);

  const ProtectedRoute: React.FC<{ isAllowed: boolean, redirectPath: string, children: ReactNode}> = ({ isAllowed, redirectPath, children }) => {
    if (!isAllowed) {
      return <Navigate to={redirectPath} replace />;
    }
    
    return children ? children : <Outlet />;
  };


  return loading ? (
    <Loader />
  ) : (
    <>
      <Routes>
        <Route
          index
          path="/"
          element={
            <>
              <UserLoader>
                <PageTitle title="Calendar | TailAdmin - Tailwind CSS Admin Dashboard Template" />
                <Homepage />
              </UserLoader>
            </>
          }
        />

        <Route
          path="/settings"
          element={
            <>
              <UserLoader>
                <PageTitle title="Settings | TailAdmin - Tailwind CSS Admin Dashboard Template" />
                <Settings />
              </UserLoader>
            </>
          }
        />

        <Route
          path="/auth/signin"
          element={
            <UserLoader>
              <ProtectedRoute isAllowed={!isLoggedIn && user == null} redirectPath='/countries'>
                <SignIn />
              </ProtectedRoute>
            </UserLoader>
          }
        />

        <Route
          path="/auth/signup"
          element={
            <UserLoader>
              <ProtectedRoute isAllowed={!isLoggedIn && user == null} redirectPath='/countries'>
                <SignUp />
              </ProtectedRoute>
            </UserLoader>
          }
        />


        <Route
          path="/countries"
          element={
            <>
              <UserLoader>
                <ProtectedRoute isAllowed={isLoggedIn} redirectPath='/auth/signin'>
                  <Countries />
                </ProtectedRoute>
              </UserLoader>
            </>
          }
        />

        <Route
          path="/create"
          element={
            <UserLoader>
              <ProtectedRoute isAllowed={isLoggedIn && user && user.role == 'Admin' ? true : false} redirectPath='/auth/signin'>
                <CreateCountry />
              </ProtectedRoute>
            </UserLoader>
          }
        />
        
        <Route
          path="/edit/:id"
          element={
            <UserLoader>
              <ProtectedRoute isAllowed={isLoggedIn && user && user.role == 'Admin' ? true : false} redirectPath='/auth/signin'>
                <PageTitle title="Signup | TailAdmin - Tailwind CSS Admin Dashboard Template" />
                <EditCountry />
              </ProtectedRoute>
            </UserLoader>
          }
        />
        
        <Route
          path="/cities"
          element={
            <>
              <UserLoader>
                <ProtectedRoute isAllowed={isLoggedIn} redirectPath='/auth/signin'>
                  <Cities />
                </ProtectedRoute>
              </UserLoader>
            </>
          }
        />

        <Route
          path="/cities/create"
          element={
            <UserLoader>
              <ProtectedRoute isAllowed={isLoggedIn && user && user.role == 'Admin' ? true : false} redirectPath='/auth/signin'>
                <CreateCity />
              </ProtectedRoute>
            </UserLoader>
          }
        />

        <Route
          path="/cities/edit/:id"
          element={
            <UserLoader>
              <ProtectedRoute isAllowed={isLoggedIn && user && user.role == 'Admin' ? true : false} redirectPath='/auth/signin'>
                <EditCity />
              </ProtectedRoute>
            </UserLoader>
          }
        />
        
        <Route
          path="/levels"
          element={
            <>
              <UserLoader>
                <ProtectedRoute isAllowed={isLoggedIn} redirectPath='/auth/signin'>
                  <Levels />
                </ProtectedRoute>
              </UserLoader>
            </>
          }
        />

        <Route
          path="/levels/create"
          element={
            <UserLoader>
              <ProtectedRoute isAllowed={isLoggedIn && user && user.role == 'Admin' ? true : false} redirectPath='/auth/signin'>
                <CreateLevel />
              </ProtectedRoute>
            </UserLoader>
          }
        />

        <Route
          path="/levels/edit/:id"
          element={
            <UserLoader>
              <ProtectedRoute isAllowed={isLoggedIn && user && user.role == 'Admin' ? true : false} redirectPath='/auth/signin'>
                <EditLevel />
              </ProtectedRoute>
            </UserLoader>
          }
        />

        <Route
          path="/specializations"
          element={
            <>
              <UserLoader>
                <ProtectedRoute isAllowed={isLoggedIn} redirectPath='/auth/signin'>
                  <Specializations />
                </ProtectedRoute>
              </UserLoader>
            </>
          }
        />

        <Route
          path="/specializations/create"
          element={
            <UserLoader>
              <ProtectedRoute isAllowed={isLoggedIn && user && user.role == 'Admin' ? true : false} redirectPath='/auth/signin'>
                <CreateSpecialization />
              </ProtectedRoute>
            </UserLoader>
          }
        />

        <Route
          path="/specializations/edit/:id"
          element={
            <UserLoader>
              <ProtectedRoute isAllowed={isLoggedIn && user && user.role == 'Admin' ? true : false} redirectPath='/auth/signin'>
                <EditSpecialization />
              </ProtectedRoute>
            </UserLoader>
          }
        />

        <Route
          path="/services"
          element={
            <>
              <UserLoader>
                <ProtectedRoute isAllowed={isLoggedIn} redirectPath='/auth/signin'>
                  <Services />
                </ProtectedRoute>
              </UserLoader>
            </>
          }
        />

        <Route
          path="/services/create"
          element={
            <UserLoader>
              <ProtectedRoute isAllowed={isLoggedIn && user && user.role == 'Admin' ? true : false} redirectPath='/auth/signin'>
                <CreateService />
              </ProtectedRoute>
            </UserLoader>
          }
        />

        <Route
          path="/services/edit/:id"
          element={
            <UserLoader>
              <ProtectedRoute isAllowed={isLoggedIn && user && user.role == 'Admin' ? true : false} redirectPath='/auth/signin'>
                <EditService />
              </ProtectedRoute>
            </UserLoader>
          }
        />

      </Routes>
    </>
  );
}

export default App;
