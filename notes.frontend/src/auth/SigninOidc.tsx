import { useNavigate } from "react-router-dom";
import { FC, useEffect } from 'react';
import { signinRedirectCallback } from './user-service';

const SigninOidc: FC<{}> = () => {
    const navigate = useNavigate();
    useEffect(() => {
        async function signinAsync() {
            await signinRedirectCallback();
            navigate('/');
        }
        signinAsync();
    }, [navigate]);

    return <div>Redirecting...</div>
};

export default SigninOidc;