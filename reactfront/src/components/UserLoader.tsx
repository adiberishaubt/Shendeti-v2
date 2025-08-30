import { ReactNode, useEffect } from 'react';
import { getMyUser, setFalseLoading } from '../slices/userSlice';
import { useAppDispatch, useAppSelector } from '../hooks/hooks';
import Cookies from "js-cookie"
import Loader from '../common/Loader';

export const UserLoader = (props: { children?: ReactNode }) => {
    const { isLoading, isLoggedIn } = useAppSelector((store) => store.user);
    const dispatch = useAppDispatch();
    
    useEffect(() => {
        if(Cookies.get('refreshToken') != null) {
            dispatch(getMyUser());
        } else {
            dispatch(setFalseLoading())
        }
    }, []);

    if (isLoading) {
        return <Loader />;
    }

    return (
        <div>
            {props.children}
        </div>
    )
};
