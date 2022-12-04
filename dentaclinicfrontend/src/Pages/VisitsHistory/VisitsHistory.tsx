import {
  Button,
  Table,
  TableContainer,
  Tbody,
  Td,
  Th,
  Thead,
  Tr,
  useToast,
} from "@chakra-ui/react";
import jwtDecode from "jwt-decode";
import moment from "moment";
import React, { useCallback, useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import { FreeVisit } from "../VisitsAdminPanel/types";
import { FeedbackModal } from "./FeedbackModal";

interface CardType {
  id: number;
  name: string;
  surname: string;
}

const VisitsHistory = () => {
  const [visits, setVisits] = useState<FreeVisit[]>([]);
  const [openFeedback, setOpenFeedback] = useState(false);
  const [feedback, setFeedback] = useState("");
  const [visitIdFeedback, setVisitIdFeedback] = useState<number>();
  const token = localStorage.getItem("accessToken");
  const userId = token && (jwtDecode(token as string) as any);
  const navigate = useNavigate();
  const toast = useToast();
  const getVisits = useCallback(async () => {
    const myServices = await fetch(
      `https://dentaclinic20221015140303.azurewebsites.net/api/v1/patientCards/${userId.sub}/visits`,
      {
        headers: {
          "Content-Type": "application/json",
          Authorization: `Bearer ${token}`,
        },
        method: "GET",
      }
    );
    if (myServices.status !== 200) {
      if (myServices.status === 401) {
        navigate("/");
      }
    }
    const allVisits = await myServices.json();
    setVisits(allVisits);
  }, []);

  const handleFeedbackChange = (
    e: React.ChangeEvent<HTMLInputElement>
  ): void => {
    const value = e.target.value;
    setFeedback(value);
  };

  const save = async (e: React.FormEvent<HTMLFormElement>): Promise<void> => {
    e.preventDefault();
    const response = await fetch(
      `https://dentaclinic20221015140303.azurewebsites.net/api/v1/patientCards/${userId.sub}/visits/${visitIdFeedback}/feedbacks`,
      {
        headers: {
          "Content-Type": "application/json",
          Authorization: `Bearer ${token}`,
        },
        body: JSON.stringify({ feedback: feedback }),
        method: "POST",
      }
    );
    if (response.status === 201) {
      toast({
        position: "bottom-right",
        title: "Atsiliepimas išsaugotas",
        status: "success",
        isClosable: true,
      });
    }
  };

  const cancelVisit = async (
    e: React.MouseEvent<HTMLButtonElement, MouseEvent>,
    visitId: number
  ): Promise<void> => {
    e.preventDefault();
    const response = await fetch(
      `https://dentaclinic20221015140303.azurewebsites.net/api/v1/patientCards/${userId.sub}/visits/${visitId}`,
      {
        headers: {
          "Content-Type": "application/json",
          Authorization: `Bearer ${token}`,
        },
        method: "DELETE",
      }
    );
    if (response.status === 204) {
      toast({
        position: "bottom-right",
        title: "Vizitas atšauktas",
        status: "success",
        isClosable: true,
      });
      window.location.reload();
    }
  };

  const closeModal = () => {
    setOpenFeedback(false);
  };

  const openFeedbackForm = (visitId: number): void => {
    setOpenFeedback(true);
    setVisitIdFeedback(visitId);
  };

  useEffect(() => {
    getVisits();
  }, []);
  return (
    <>
      <TableContainer m={20}>
        <Table variant="simple">
          <Thead>
            <Tr>
              <Th>Apsilankymo data</Th>
              <Th>Laikas</Th>
              <Th>Paslauga</Th>
              <Th>Odontologas</Th>
              <Th></Th>
            </Tr>
          </Thead>
          <Tbody>
            {visits.map((visit) => {
              return (
                <Tr>
                  <Td>{moment(visit.date).format("YYYY-MM-DD")}</Td>
                  <Td>{visit.time}</Td>
                  <Td>{visit.service}</Td>
                  <Td>{visit.doctorFullName}</Td>
                  <Td>
                    {moment(visit.date).format("YYYY-MM-DD") >
                    moment(new Date()).format("YYYY-MM-DD") ? (
                      <Button onClick={(e) => cancelVisit(e, visit.id)}>
                        Atšaukti
                      </Button>
                    ) : (
                      <Button onClick={(e) => openFeedbackForm(visit.id)}>
                        Palikti atsiliepimą
                      </Button>
                    )}
                  </Td>
                </Tr>
              );
            })}
          </Tbody>
        </Table>
      </TableContainer>
      <FeedbackModal
        isOpen={openFeedback}
        closeModal={closeModal}
        handleFeedbackChange={handleFeedbackChange}
        save={save}
      />
    </>
  );
};

export { VisitsHistory };
