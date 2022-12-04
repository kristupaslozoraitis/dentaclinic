import { Card, CardHeader, CardBody } from "@chakra-ui/card";
import {
  SimpleGrid,
  Flex,
  Button,
  Box,
  Heading,
  Stack,
  StackDivider,
  Text,
  useToast,
} from "@chakra-ui/react";
import moment from "moment";
import React, { FormEvent, useCallback, useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import { NewVisitModal } from "./NewVisitModal";
import { ServiceModal } from "./ServiceModal";
import { FreeVisit, Service } from "./types";

const VisitsAdminPanel = () => {
  const [services, setServices] = useState<Service[]>([]);
  const [freeVisits, setFreeVisits] = useState<FreeVisit[]>([]);
  const [date, setDate] = useState<Date>(new Date());
  const [time, setTime] = useState<string>("");
  const [serviceId, setServiceId] = useState<number>();
  const [isOpenServices, setIsOpenServices] = useState(false);
  const [isOpenNewService, setIsOpenNewService] = useState(false);
  const [isNewVisitOpen, setIsNewVisitOpen] = useState(false);
  const [isEditing, setIsEditing] = useState(false);
  const [editIndex, setEditIndex] = useState<number>();
  const [deleteAction, setDeleteAction] = useState(false);
  const [newService, setNewService] = useState("");
  const [isLoading, setIsLoading] = useState(false);
  const toast = useToast();
  const navigate = useNavigate();

  const getData = useCallback(async () => {
    var token = localStorage.getItem("accessToken");
    const myServices = await fetch("https://dentaclinic20221015140303.azurewebsites.net/api/v1/services", {
      headers: {
        "Content-Type": "application/json",
        Authorization: `Bearer ${token}`,
      },
      method: "GET",
    });
    if (myServices.status !== 200) {
      if (myServices.status === 401) {
        navigate("/");
      }
    }
    const allServices = await myServices.json();
    setServices(allServices);
    services.length > 0 && setServiceId(services[0].id);
    const myVisits = await fetch(
      "https://dentaclinic20221015140303.azurewebsites.net/api/v1/freeVisits/admin",
      {
        headers: {
          "Content-Type": "application/json",
          Authorization: `Bearer ${token}`,
        },
        method: "GET",
      }
    );
    const allVisits = await myVisits.json();
    setFreeVisits(allVisits);
    setIsLoading(false);
  }, []);

  const onSubmit = async (e: FormEvent<HTMLFormElement>): Promise<void> => {
    e.preventDefault();
    var token = localStorage.getItem("accessToken");
    const url = isEditing
      ? `https://dentaclinic20221015140303.azurewebsites.net/api/v1/freeVisits/${editIndex}`
      : "https://dentaclinic20221015140303.azurewebsites.net/api/v1/freeVisits";
    const response = await fetch(url, {
      headers: {
        "Content-Type": "application/json",
        Authorization: `Bearer ${token}`,
      },
      body: JSON.stringify({ date, time, serviceId }),
      method: isEditing ? "PUT" : "POST",
    });
    if (response.status === 200 || response.status === 201) {
      toast({
        position: "bottom-right",
        title: isEditing ? "Visitas atnaujintas" : "Visitas sukurtas",
        status: "success",
        isClosable: true,
      });
      setIsNewVisitOpen(false);
    }
  };

  const onSave = async (e: FormEvent<HTMLFormElement>): Promise<void> => {
    e.preventDefault();
    var token = localStorage.getItem("accessToken");
    const response = await fetch("https://dentaclinic20221015140303.azurewebsites.net/api/v1/services", {
      headers: {
        "Content-Type": "application/json",
        Authorization: `Bearer ${token}`,
      },
      body: JSON.stringify(services),
      method: "PUT",
    });
    if (response.status === 200) {
      toast({
        position: "bottom-right",
        title: "Paslaugos atnaujintos",
        status: "success",
        isClosable: true,
      });
      setIsOpenServices(false);
    }
  };

  const onCreate = async (e: FormEvent<HTMLFormElement>): Promise<void> => {
    e.preventDefault();
    var token = localStorage.getItem("accessToken");
    const response = await fetch("https://dentaclinic20221015140303.azurewebsites.net/api/v1/services", {
      headers: {
        "Content-Type": "application/json",
        Authorization: `Bearer ${token}`,
      },
      body: JSON.stringify({ name: newService }),
      method: "POST",
    });
    if (response.status === 201) {
      toast({
        position: "bottom-right",
        title: "Paslauga sukurta",
        status: "success",
        isClosable: true,
      });
      setIsOpenServices(false);
    }
  };

  const deleteService = async (
    e: React.MouseEvent<HTMLButtonElement, MouseEvent>,
    serviceId: number
  ): Promise<void> => {
    e.preventDefault();
    var token = localStorage.getItem("accessToken");
    const response = await fetch(
      `https://dentaclinic20221015140303.azurewebsites.net/api/v1/services/${serviceId}`,
      {
        headers: {
          "Content-Type": "application/json",
          Authorization: `Bearer ${token}`,
        },
        method: "DELETE",
      }
    );
    if (response.status === 204) {
      setDeleteAction(!deleteAction);
      toast({
        position: "bottom-right",
        title: "Paslauga sėkmingai ištrinta",
        status: "error",
        isClosable: true,
      });
    } else {
      toast({
        position: "bottom-right",
        title: "Paslauga negali būti ištrinta - ji yra naudojama",
        status: "error",
        isClosable: true,
      });
    }
  };
  const handleNewServiceChange = (
    e: React.ChangeEvent<HTMLInputElement>
  ): void => {
    const value = e.target.value;
    setNewService(value);
  };

  const getRoles = useCallback(async () => {
    setIsLoading(true);
    var token = localStorage.getItem("accessToken");
    const data = await fetch("https://dentaclinic20221015140303.azurewebsites.net/api/me", {
      headers: {
        "Content-Type": "application/json",
        Authorization: `Bearer ${token}`,
      },
      method: "GET",
    });
    const roles = await data.json();
    if (!roles.includes("Odontologist")) {
      navigate("/");
    }
  }, []);

  useEffect(() => {
    getRoles();
    getData();
  }, [isOpenServices, isNewVisitOpen, deleteAction]);

  const closeModal = () => {
    setIsOpenServices(false);
    setIsOpenNewService(false);
  };

  const closeVisitModal = () => {
    setIsNewVisitOpen(false);
  };

  const handleServiceChange = (value: string, index: number): void => {
    const tempServices = [...services];
    tempServices[index].name = value;
    setServices(tempServices);
  };

  const handleDateChange = (e: React.ChangeEvent<HTMLInputElement>): void => {
    const value = new Date(e.target.value);
    setDate(value);
  };

  const handleTimeChange = (e: React.ChangeEvent<HTMLInputElement>): void => {
    const value = e.target.value;
    setTime(value);
  };

  const handleServiceIdChange = (
    e: React.ChangeEvent<HTMLSelectElement>
  ): void => {
    const value = e.target.value as unknown as number;
    setServiceId(value);
  };

  const editVisit = (index: number): void => {
    setIsEditing(true);
    const editingVisit = freeVisits[index];
    setDate(editingVisit.date);
    setEditIndex(editingVisit.id);
    setTime(editingVisit.time);
    setServiceId(editingVisit.serviceId);
    setIsNewVisitOpen(true);
  };

  return isLoading ? null : (
    <>
      <Box m={5}>
        <Flex gap={10} mt={5} justifyContent="end">
          <Button onClick={() => setIsOpenServices(true)}>
            Mano paslaugos
          </Button>
          <Button onClick={() => setIsOpenNewService(true)}>
            Pridėti paslaugą
          </Button>
          <Button onClick={() => setIsNewVisitOpen(true)}>
            Pridėti vizitą
          </Button>
        </Flex>
        <SimpleGrid columns={3} spacing={10} mt={5}>
          {freeVisits.map((freeVisit, index) => {
            return (
              <Card key={index} border="solid" borderRadius={10}>
                <CardHeader m={5} textAlign="center">
                  <Heading size="md">{freeVisit.doctorFullName}</Heading>
                </CardHeader>

                <CardBody m={5}>
                  <Stack divider={<StackDivider />} spacing="4">
                    <Box>
                      <Heading size="xs" textTransform="uppercase">
                        Data
                      </Heading>
                      <Text pt="2" fontSize="sm">
                        {moment(freeVisit.date).format("YYYY-MM-DD")}
                      </Text>
                    </Box>
                    <Box>
                      <Heading size="xs" textTransform="uppercase">
                        Laikas
                      </Heading>
                      <Text pt="2" fontSize="sm">
                        {freeVisit.time}
                      </Text>
                    </Box>
                    <Box>
                      <Heading size="xs" textTransform="uppercase">
                        Paslauga
                      </Heading>
                      <Text pt="2" fontSize="sm">
                        {freeVisit.service}
                      </Text>
                    </Box>
                    <Flex
                      textAlign="right"
                      justifyContent="space-between"
                      alignItems="center"
                    >
                      {freeVisit.patient.length > 0 ? (
                        <Text fontSize="lg">Pacientas {freeVisit.patient}</Text>
                      ) : (
                        <Button onClick={() => editVisit(index)}>
                          Redaguoti vizitą
                        </Button>
                      )}
                    </Flex>
                  </Stack>
                </CardBody>
              </Card>
            );
          })}
        </SimpleGrid>
      </Box>
      <ServiceModal
        isOpen={isOpenServices || isOpenNewService}
        closeModal={closeModal}
        services={isOpenNewService ? [] : services}
        save={onSave}
        handleServiceChange={handleServiceChange}
        deleteService={deleteService}
        handleNewServiceChange={handleNewServiceChange}
        onCreate={onCreate}
      />
      <NewVisitModal
        isOpen={isNewVisitOpen}
        closeModal={closeVisitModal}
        save={onSubmit}
        services={services}
        handleDateChange={handleDateChange}
        handleTimeChange={handleTimeChange}
        handleServiceIdChange={handleServiceIdChange}
        date={date}
        time={time}
        serviceId={serviceId as number}
      />
    </>
  );
};

export { VisitsAdminPanel };
