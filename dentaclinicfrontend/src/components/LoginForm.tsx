import {
  Box,
  Button,
  FormControl,
  FormLabel,
  Input,
  useToast,
} from "@chakra-ui/react";
import { ChangeEvent, FormEvent, ReactElement, useState } from "react";

interface LoginFormProps {
  closeModal: () => void;
}

const LoginForm = ({
  closeModal,
}: LoginFormProps): ReactElement<LoginFormProps> => {
  const [email, setEmail] = useState<string>();
  const [password, setPassword] = useState<string>();
  const toast = useToast();

  const onEmailChange = (e: ChangeEvent<HTMLInputElement>): void => {
    setEmail(e.target.value as string);
  };

  const onPasswordChange = (e: ChangeEvent<HTMLInputElement>): void => {
    setPassword(e.target.value as string);
  };

  const Login = async (e: FormEvent<HTMLFormElement>): Promise<void> => {
    e.preventDefault();
    const data = await fetch("https://dentaclinic20221015140303.azurewebsites.net/api/login", {
      headers: {
        "Content-Type": "application/json",
      },
      method: "POST",
      body: JSON.stringify({ email, password }),
    });
    if (data.status === 200) {
      toast({
        position: "bottom-right",
        title: "Prisijungimas sėkmingas",
        status: "success",
        isClosable: true,
      });
    } else {
      toast({
        position: "bottom-right",
        title: "El. paštas arba slaptažodis neteisingi",
        status: "error",
        isClosable: true,
      });
    }
    const token = await data.json();
    localStorage.setItem("accessToken", token.accessToken);
    window.location.reload();
  };

  return (
    <form onSubmit={(e) => Login(e)}>
      <FormControl>
        <FormLabel>El. pašto adresas</FormLabel>
        <Input
          type="email"
          required
          onChange={(e) => {
            onEmailChange(e);
          }}
        />
        <FormLabel>Slaptažodis</FormLabel>
        <Input
          type="password"
          required
          onChange={(e) => {
            onPasswordChange(e);
          }}
        />
      </FormControl>
      <Box textAlign="center">
        <Button m={5} type="submit">
          Prisijungti
        </Button>
      </Box>
    </form>
  );
};

export { LoginForm };
