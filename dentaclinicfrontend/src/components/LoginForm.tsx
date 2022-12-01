import { Box, Button, FormControl, FormLabel, Input } from '@chakra-ui/react';
import jwtDecode from 'jwt-decode';
import React, { ChangeEvent, FormEvent, useState } from 'react';

const LoginForm = () => {
    const [email, setEmail] = useState<string>();
    const [password, setPassword] = useState<string>();

    const onEmailChange = (e: ChangeEvent<HTMLInputElement>): void => {
        setEmail(e.target.value as string);
    }

    const onPasswordChange = (e: ChangeEvent<HTMLInputElement>): void => {
        setPassword(e.target.value as string);
    }

    const Login = async (e: FormEvent<HTMLFormElement>): Promise<void> => {
        e.preventDefault();
        const data = await fetch("https://localhost:7257/api/login", {
            headers: {
                'Content-Type': 'application/json',
            },
            method: 'POST',
            body: JSON.stringify({ email, password })
        })
        const token = await data.json();
        localStorage.setItem("accessToken", token.accessToken);
    }

    return (
        <form onSubmit={(e) => Login(e)}>
            <FormControl>
                <FormLabel>El. pašto adresas</FormLabel>
                <Input type='email' onChange={(e) => { onEmailChange(e) }} />
                <FormLabel>Slaptažodis</FormLabel>
                <Input type='password' onChange={(e) => { onPasswordChange(e) }} />
            </FormControl>
            <Box textAlign="center">
                <Button m={5} type='submit'>Prisijungti</Button>
            </Box>
        </form>
    )
}

export { LoginForm }