// pages/index.js
'use client'
import { useEffect } from 'react';
import { Container, Row, Col, Card, Carousel } from 'react-bootstrap';

const HomePage = () => {
  // Dữ liệu cho các thẻ Card
  const cardData = [
    {
      title: "Image One",
      description: "This is the description for image one.",
      imgSrc: "/cardData1.png",
    },
    {
      title: "Image Two",
      description: "This is the description for image two.",
      imgSrc: "/cardData5.png",
    },
    {
      title: "Image Three",
      description: "This is the description for image three.",
      imgSrc: "/cardData9.png",
    },
    {
      title: "Image Four",
      description: "This is the description for image four.",
      imgSrc: "cardData2.png",
    },
    {
      title: "Image Five",
      description: "This is the description for image five.",
      imgSrc: "cardData6.png",
    },
    {
      title: "Image Six",
      description: "This is the description for image six.",
      imgSrc: "cardData10.png",
    },
  ];

  // Dữ liệu cho Carousel
  const carouselData = [
    {
      imgSrc: "/slider1.png",
      caption: "Welcome to Our Site!",
    },
    {
      imgSrc: "/slider2.png",
      caption: "Discover Amazing Features!",
    },
    {
      imgSrc: "/slider3.png",
      caption: "Join Us Today!",
    },
  ];

  return (
    <Container className="mt-5">
      {/* Slider (Carousel) */}
      <Carousel className="mb-4">
        {carouselData.map((item, index) => (
          <Carousel.Item key={index}>
            <img
              className="d-block w-100"
              src={item.imgSrc}
              alt={`Slide ${index}`}
            />
            <Carousel.Caption>
              <h3>{item.caption}</h3>
            </Carousel.Caption>
          </Carousel.Item>
        ))}
      </Carousel>

      {/* Giới thiệu */}
      <Row className="mb-4">
        <Col>
          <h2 className="text-center">About Us</h2>
          <p>
            We are a leading company in the field of sports club management, dedicated to providing exceptional services and products to our clients. Our team is passionate and committed to excellence, always ready to support clubs in their development and performance enhancement.

            With extensive experience and deep industry knowledge, we continuously strive to expand our vision and innovate to deliver optimal solutions for sports clubs. We believe that every club has its unique potential, and our mission is to help them realize that potential.

            Join us as we explore new horizons and build a brighter future for sports!
          </p>
        </Col>
      </Row>

      {/* Danh sách các thẻ Card */}
      <h2 className="text-center mb-4">Our Products</h2>
      <Row>
        {cardData.map((item, index) => (
          <Col md={4} key={index} className="mb-4">
            <Card>
              <Card.Img variant="top" src={item.imgSrc} />
              <Card.Body>
                <Card.Title>{item.title}</Card.Title>
                <Card.Text>{item.description}</Card.Text>
                <Card.Link href="#">View Details</Card.Link>
              </Card.Body>
            </Card>
          </Col>
        ))}
      </Row>

      {/* Footer */}
      <footer className="text-center mt-5 mb-3">
        <p>&copy; {new Date().getFullYear()} Your Company. All rights reserved.</p>
      </footer>
    </Container>
  );
};

export default HomePage;
