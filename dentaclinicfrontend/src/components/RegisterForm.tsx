import {
  Button,
  Flex,
  FormControl,
  FormLabel,
  Input,
  useToast,
} from "@chakra-ui/react";
import { useSteps, Steps, Step } from "chakra-ui-steps";
import { ChangeEvent, FormEvent, ReactElement, useState } from "react";

const steps = [{ label: "1 žingsnis" }, { label: "2 žingsnis" }];

interface RegisterFormProps {
  closeModal: () => void;
}

const RegisterForm = ({
  closeModal,
}: RegisterFormProps): ReactElement<RegisterFormProps> => {
  const [email, setEmail] = useState<string>();
  const [password, setPassword] = useState<string>();
  const [name, setName] = useState<string>();
  const [surname, setSurname] = useState<string>();
  const [birthDate, setBirthDate] = useState<Date>();
  const [personalNumber, setPersonalNumber] = useState<number>();
  const [homeAddress, setHomeAddress] = useState<string>();
  const [phoneNumber, setPhoneNumber] = useState<string>();
  const [height, setHeight] = useState<number>();
  const [weight, setWeight] = useState<number>();
  const toast = useToast();

  const { nextStep, prevStep, activeStep } = useSteps({
    initialStep: 0,
  });
  const onEmailChange = (e: ChangeEvent<HTMLInputElement>): void => {
    setEmail(e.target.value as string);
  };

  const onPasswordChange = (e: ChangeEvent<HTMLInputElement>): void => {
    setPassword(e.target.value as string);
  };

  const onNameChange = (e: ChangeEvent<HTMLInputElement>): void => {
    setName(e.target.value as string);
  };

  const onSurnameChange = (e: ChangeEvent<HTMLInputElement>): void => {
    setSurname(e.target.value as string);
  };

  const onBirthDateChange = (e: ChangeEvent<HTMLInputElement>): void => {
    setBirthDate(e.target.value as unknown as Date);
  };

  const onPersonalNumberChange = (e: ChangeEvent<HTMLInputElement>): void => {
    setPersonalNumber(e.target.value as unknown as number);
  };

  const onHomeAddressChange = (e: ChangeEvent<HTMLInputElement>): void => {
    setHomeAddress(e.target.value as string);
  };

  const onPhoneNumberChange = (e: ChangeEvent<HTMLInputElement>): void => {
    setPhoneNumber(e.target.value as string);
  };

  const onWeightChange = (e: ChangeEvent<HTMLInputElement>): void => {
    setWeight(e.target.value as unknown as number);
  };

  const onHeightChange = (e: ChangeEvent<HTMLInputElement>): void => {
    setHeight(e.target.value as unknown as number);
  };

  const Register = async (e: FormEvent<HTMLFormElement>): Promise<void> => {
    e.preventDefault();
    const data = await fetch("https://dentaclinic20221015140303.azurewebsites.net/api/register", {
      headers: {
        "Content-Type": "application/json",
      },
      method: "POST",
      body: JSON.stringify({
        email,
        password,
        userInformation: {
          name,
          surname,
          birthDate,
          personalNumber,
          homeAddress,
          phoneNumber,
          height,
          weight,
        },
      }),
    });
    if (data.status === 201) {
      toast({
        position: "bottom-right",
        title: "Registracija sėkminga. Galite prisijungti",
        status: "success",
        isClosable: true,
      });
      closeModal();
    }
    const token = await data.json();
    console.log(token);
  };

  return (
    <Flex flexDir="column" width="100%">
      <form onSubmit={(e) => Register(e)}>
        <FormControl>
          <Steps activeStep={activeStep}>
            {steps.map(({ label }, index) => (
              <Step label={label} key={label}>
                {index == 0 ? (
                  <>
                    <FormLabel>El. pašto adresas</FormLabel>
                    <Input
                      type="email"
                      onChange={(e) => {
                        onEmailChange(e);
                      }}
                    />
                    <FormLabel>Slaptažodis</FormLabel>
                    <Input
                      type="password"
                      onChange={(e) => {
                        onPasswordChange(e);
                      }}
                    />
                  </>
                ) : (
                  <>
                    <FormLabel>Vardas</FormLabel>
                    <Input
                      type="text"
                      onChange={(e) => {
                        onNameChange(e);
                      }}
                    />
                    <FormLabel>Pavardė</FormLabel>
                    <Input
                      type="text"
                      onChange={(e) => {
                        onSurnameChange(e);
                      }}
                    />
                    <FormLabel>Gimimo data</FormLabel>
                    <Input
                      type="date"
                      onChange={(e) => {
                        onBirthDateChange(e);
                      }}
                    />
                    <FormLabel>Asmens kodas</FormLabel>
                    <Input
                      type="number"
                      onChange={(e) => {
                        onPersonalNumberChange(e);
                      }}
                    />
                    <FormLabel>Gyv. adresas</FormLabel>
                    <Input
                      type="text"
                      onChange={(e) => {
                        onHomeAddressChange(e);
                      }}
                    />
                    <FormLabel>Tel. numeris</FormLabel>
                    <Input
                      type="text"
                      onChange={(e) => {
                        onPhoneNumberChange(e);
                      }}
                    />
                    <FormLabel>Ūgis</FormLabel>
                    <Input
                      type="number"
                      onChange={(e) => {
                        onHeightChange(e);
                      }}
                    />
                    <FormLabel>Svoris</FormLabel>
                    <Input
                      type="number"
                      onChange={(e) => {
                        onWeightChange(e);
                      }}
                    />
                  </>
                )}
              </Step>
            ))}
          </Steps>
        </FormControl>
        <Flex width="100%" justify="flex-end" mt={5}>
          <Button
            isDisabled={activeStep === 0}
            mr={4}
            onClick={prevStep}
            size="sm"
            variant="ghost"
          >
            Atgal
          </Button>
          {activeStep === steps.length - 1 ? (
            <Button type="submit" size="sm">
              Registruotis
            </Button>
          ) : (
            <Button size="sm" onClick={nextStep}>
              Toliau
            </Button>
          )}
        </Flex>
      </form>
    </Flex>
  );
};

export { RegisterForm };
